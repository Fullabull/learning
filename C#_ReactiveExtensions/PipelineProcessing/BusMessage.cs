public IObservable<RawBusMessage> StartConsuming(CancellationToken token, TimeSpan interval)
{
    // run the producer on a separate dedicated thread so we don't get a jittery interval from running on the thread pool
    var scheduler = new NewThreadScheduler(ts => new Thread(ts) { Name = "DataBusPoller" });

    var source = Observable.Generate(Read(),
        x => !token.IsCancellationRequested, // run until cancelled
        x => Read(), // consume the machine bus interface
        x => x, // return the message as is
        x => interval, // execute Read() on the supplied interval
        scheduler);

    return source;
}

private RawBusMessage Read()
{
    _counter++;
    var reading = _dataBus.Read();

    var message = new RawBusMessage();
    message.Data = reading.Data;
    message.ReadingTime = new DateTime(reading.Ticks);
    message.Counter = _counter;

    return message;
}

public async Task StartPipelineAsync(CancellationToken token)
{
    _decoder.LoadSensorConfigs();

    // Step 1 - Create our producer as a cold observable
    var source = _dataBusReader.StartConsuming(token, TimeSpan.FromMilliseconds(10));

    // Step 2 - Add file writing and decoding stages to our cold observable pipeline
    var writeStream = source.ObserveOn(ThreadPoolScheduler.Instance)
                        .Select(x => Observable.FromAsync(async () => 
    {
        await _messageFileWriter.WriteAsync(x);
        return x;
    })).Concat();

    var decodedStream = writeStream.Select(x => 
    {
        return _decoder.Decode(x).ToObservable();
    }).Concat();

    // Step 3 - Create a hot observable that acts as a broadcast 
    // and allows multiple subscribers without duplicating the work of the producer
    var multiCastStream = Observable.Publish(decodedStream);

    // Step 4 - Create our subscriptions that perform work on the decoded messages
    // create a subscription to the hot obeservable that buffers in 1 second periods and performs up to 4 concurrent db writes
    var dbPersistenceComplete = false;
    var dbPersistenceSub = multiCastStream
                        .Buffer(TimeSpan.FromSeconds(1))
                        .Where(messages => messages.Any())
                        .Select(messages => Observable.FromAsync(async () => await _dbPersister.PersistAsync(messages)))
                        .Merge(4) // up to 4 concurrent executions of PersistAsync
                        .Subscribe(
                            (Unit u) => { },
                            (Exception ex) => { Console.WriteLine("DB Persistence error: " + ex); },
                            () => 
                            {
                                dbPersistenceComplete = true;
                                Console.WriteLine("DB Persistence complete!");
                            });

    // create a subscription to the hot obeservable that buffers in 1 second periods and performs sequential processing of each batch
    bool statsFeed1Complete = false;
    var oneSecondStatsFeedSub = multiCastStream
                        .Buffer(TimeSpan.FromSeconds(1))
                        .Where(messages => messages.Any())
                        .Select(messages => Observable.FromAsync(async () => await _statsFeedPublisher.PublishAsync(messages, TimeSpan.FromSeconds(1))))
                        .Concat() // one batch at a time
                        .Subscribe(
                            (Unit u) => { },
                            (Exception ex) => { Console.WriteLine("1 Second Stats Feed Error: " + ex); },
                            () => 
                            {
                                statsFeed1Complete = true;
                                Console.WriteLine("1 Second Stats Feed Complete!");
                            });

    // create a subscription to the hot obeservable that buffers in 30 second periods and performs sequential processing of each batch
    bool statsFeed30Complete = false;
    var thirtySecondStatsFeedSub = multiCastStream
                        .Buffer(TimeSpan.FromSeconds(30))
                        .Where(messages => messages.Any())
                        .Select(messages => Observable.FromAsync(async () => await _statsFeedPublisher.PublishAsync(messages, TimeSpan.FromSeconds(30))))
                        .Concat() // one batch at a time
                        .Subscribe(
                            (Unit u) => { },
                            (Exception ex) => { Console.WriteLine("30 Second Stats Feed Error: " + ex); },
                            () => 
                            {
                                statsFeed30Complete = true;
                                Console.WriteLine("30 Second Stats Feed Error Complete!");
                            });

    // create a subscription to the hot obeservable that sequentially processes one message at a time in order
    bool realTimePubComplete = false;
    var realTimePubSub = multiCastStream
                        .Select(messages => Observable.FromAsync(async () => await _realTimeFeedPublisher.PublishAsync(messages)))
                        .Concat() // one message at a time
                        .Subscribe(
                            (Unit u) => { },
                            (Exception ex) => { Console.WriteLine("Real-time Pub Error: " + ex); },
                            () => 
                            {
                                realTimePubComplete = true;
                                Console.WriteLine("Real-time Pub Complete!");
                            });

    // Step 6. Start the producer
    multiCastStream.Connect();

    // Step 7. Keep things going until the CancellationToken gets cancelled
    while (!token.IsCancellationRequested)
        await Task.Delay(500);

    // Step 8. Safe shutdown of the pipeline
    // Wait for all subscriptions to complete their work
    while (!realTimePubComplete || !dbPersistenceComplete || !statsFeed1Complete || !statsFeed30Complete)
        await Task.Delay(500);

    Console.WriteLine("All subscribers complete!");

    // dispose of all subscriptions
    dbPersistenceSub.Dispose();
    oneSecondStatsFeedSub.Dispose();
    thirtySecondStatsFeedSub.Dispose();
    realTimePubSub.Dispose();

    // clean up the producer
    _dataBusReader.CleanUp();

    // safely clean up any other resources, for example, ZeroMQ
}

public interface IDataBusReader
{
    IObservable<RawBusMessage> StartConsuming(CancellationToken token, TimeSpan interval);
}

public interface IMessageFileWriter
{
    Task WriteAsync(RawBusMessage message);
}

public interface IDecoder
{
    void LoadSensorConfigs();
    IEnumerable<DecodedMessage> Decode(RawBusMessage message);
}

public interface IRealTimePublisher
{
    Task PublishAsync(DecodedMessage message);
}

public interface IStatsFeedPublisher
{
    Task PublishAsync(IList<DecodedMessage> messages, TimeSpan window);
}

public interface IDbPersister
{
    Task PersistAsync(IList<DecodedMessage> messages);
}
