using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxDemos.SubjectOfT2Demo
{
  public static class ExtensionMethods
  {
    // inversion of control
    public static IDisposable SubscribeTo<T>(this IObserver<T> observer,
      IObservable<T> observable)
    {
      return observable.Subscribe(observer);
    }
  }

  public class ProxyAndBroadcast
  {
    static void MainPB()
    {
      var market = new Subject<float>(); // observable
      var marketConsumer = new Subject<float>(); // observer and observable

      // isn't this api better?
      marketConsumer.SubscribeTo(market);

      // well, not really, because we need to be able to do this
      market
        .Where(x => x > 2)
        .Subscribe(marketConsumer);

      // market -----> marketConsumer
      // we subscribe to marketConsumer
      marketConsumer.Subscribe(Console.WriteLine);

      // now post something on the market
      market.OnNext(1,2,3,4);
      market.OnCompleted();
    }
  }
}