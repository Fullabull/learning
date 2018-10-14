using System;

namespace ConsoleApp2
{
    public class S02L05_ODP_IObservable
    {
        public class Market : IObservable<float>
        {
            public IDisposable Subscribe(IObserver<float> observer)
            {
                return Market.CreateWithDisposable<float>(
                    observer => observer.Subscribe(
                        x =>
                        {
                            float result;
                            try
                            {
                                result = selector(x);
                            }
                            catch (Exception exception)
                            {
                                observer.OnError(exception);
                                return;
                            }
                            observer.OnNext(result);
                        },
                        observer.OnError,
                        observer.OnCompleted));
            }
        }

        public class Program : IObserver<float>
        {
            public Program()
            {
                var market = new Market();
                market.Subscribe(this);
                Console.WriteLine("Implemented new market in Program constructor.");
            }
            static void MainS02L05(string[] args)
            {
                 // OnNext* --> ( OnCompleted | OnError )? -->
            }

            public void OnNext(float value)
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnCompleted()
            {
                throw new NotImplementedException();
            }
        }
    }
}