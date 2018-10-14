using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reactive.Disposables;

namespace RxDemos.ImplementingObservable.Simple
{
  public class Market : IObservable<float>
  {
    // list?
    // hashset
    // concurrentbag
    private ImmutableHashSet<IObserver<float>> observers = ImmutableHashSet<IObserver<float>>.Empty;

    public IDisposable Subscribe(IObserver<float> observer)
    {
      observers = observers.Add(observer);
      return Disposable.Create(() =>
      {
        observers = observers.Remove(observer);
      });
    }

    public void Publish(float price)
    {
      foreach (var o in observers)
        o.OnNext(price);
    }
  }

  public class Demo
  {
    static void MainSO(string[] args)
    {
      var market = new Market();
      var subscription = market.Subscribe(
        value => Console.WriteLine($"Got market value {value}"));

      market.Publish(123);
    }
  }
}