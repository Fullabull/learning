using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using RxDemos.Annotations;
using static System.Console;

namespace RxDemos.SequenceBasics.ConversionToObservable
{
  public class Demo
  {


    static void MainFromEnumerable(string[] args)
    {
      var items = new List<int> {1, 2, 3}; // blocking
      var source = items.ToObservable();
      source.Subscribe(WriteLine);
    }

    static void MainFromTask(string[] args)
    {
      var t = Task.Factory.StartNew(() => "Test");
      var source = t.ToObservable();
      source.Subscribe(WriteLine,
        () => WriteLine("Task is done!"));

      // mention my TPL course
    }

    class Market : INotifyPropertyChanged
    {
      private float price;

      public float Price
      {
        get => price;
        set
        {
          if (value.Equals(price)) return;
          price = value;
          OnPropertyChanged();
        }
      }


      public void ChangePrice(float newPrice)
      {
        PriceChanged?.Invoke(this, newPrice);
      }

      public event EventHandler<float> PriceChanged;
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged(
        [CallerMemberName] string propertyName = null)
      {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    static void MainFromEvents(string[] args)
    {
      var market = new Market();
      var priceChanges = Observable.FromEventPattern<float>(
        h => market.PriceChanged += h,
        h => market.PriceChanged -= h
      ); // also handle EventHandler w/o type

      // EventPattern<float>
      priceChanges.Subscribe(ep => Console.WriteLine(ep.EventArgs));

      market.ChangePrice(1);
      market.ChangePrice(1.1f);
      market.ChangePrice(1.2f);

      // Rxx?

    }

    static void MainFromDelegates(string[] args)
    {
      // 1. From delegates

      // Observable.Start lets you turn a long running func or action into a reactive seq

      var start = Observable.Start(() => // ThreadPool.QUWI
      {
        WriteLine("Starting work...");
        for (int i = 0; i < 10; i++)
        {
          Thread.Sleep(200);
          Write(".");
        }
      }); // this is done asynchronously on a threadpool thread

      for (int i = 0; i < 10; i++)
      {
        Thread.Sleep(200);
        Write("-");
      }

      start.Subscribe(
        unit => WriteLine("Got a unit"),
        () => WriteLine("Action complete")
      );
      ReadKey();

      // difference between observable.start and return:
      // observable.return is eager, observable.start is lazy

      // the point of Observable.Start is to integrate computationally heavy
      // work into a code base made up of observable sequences
    }
  }
}