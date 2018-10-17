using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace RxDemos.SequenceBasics.FunctionallyCreatingSequences
{
  public class Demo
  {
    private static IObservable<string> Blocking()
    {
      var subj = new ReplaySubject<string>();
      subj.OnNext("foo");
      subj.OnNext("bar");
      subj.OnCompleted();
      Thread.Sleep(3000);
      return subj;
    }

    private static IObservable<string> Nonblocking()
    {
      return Observable.Create<string>(observer =>
      {
        observer.OnNext("foo");
        observer.OnNext("bar");
        observer.OnCompleted();
        Thread.Sleep(3000);
        //return Disposable.Create(() => Console.WriteLine("Observer unsubscribed"));
        return Disposable.Empty;
      });
    }

    static void Main01(string[] args)
    {

    }

    static void MainCS()
    {
      // create an observable sequence from a specified subscribe method implementation
      // lets you specify a delegate any time a subscription is made

      //Blocking().Subscribe(s => Console.WriteLine($"Got {s}"));
      //Nonblocking().Subscribe(s => Console.WriteLine($"Got {s}"));

      // 2. Show Return<T>

      // 3. Returning an action instead of an IDisposable
//      var o = Observable.Create<string>(observer =>
//      {
//        var timer = new Timer(1000);
//        timer.Elapsed += (sender, e) => observer.OnNext($"tick {e.SignalTime}");
//        timer.Elapsed += TimerElapsed;
//        timer.Start();
//        return Disposable.Empty;
//      });
//
//      var sub = o.Subscribe(Console.WriteLine);
//      Console.ReadLine();
//
//      sub.Dispose();
//      // want to do timer.Dispose here
//
//      Console.ReadLine(); // still getting the tocks
//                          // have not released the 2nd event handler;
//                          // have not disposed of the timer


      var o = Observable.Create<string>(
        observer =>
        {
          var timer = new Timer(1000);
          timer.Elapsed += (sender, e) => observer.OnNext($"tick {e.SignalTime}");
          timer.Elapsed += TimerElapsed;
          timer.Start();

          // return a lambda that removes the timer
          return () =>
          {
            timer.Elapsed -= TimerElapsed;
            timer.Dispose();
          };
        });

      var sub = o.Subscribe(Console.WriteLine);
      Console.ReadLine();

      sub.Dispose();
      // not getting tocks here

      Console.ReadLine();
    }

    private static void TimerElapsed(object sender, ElapsedEventArgs e)
    {
      Console.WriteLine($"tock {e.SignalTime}");
    }

    public static IObservable<T> Return<T>(T value)
    {
      return Observable.Create<T>(x =>
      {
        x.OnNext(value);
        x.OnCompleted();
        return Disposable.Empty;
      });
    }

    // simple factory methods
    static void SequenceCreation(string[] args)
    {
      // shortcuts to creating ReplaySubject
      var meaningOfLife = Observable.Return<int>(42); // ^^^ try recreating above
      meaningOfLife.Subscribe(Console.WriteLine);

      // is equivalent to
      var mol = new ReplaySubject<int>();
      mol.OnNext(42);
      mol.OnCompleted();

      var empty = Observable.Empty<int>();
      // basically, same as a replay subject that's immediatelly completed

      var never = Observable.Never<int>();
      // no items, doesn't terminate

      var throws = Observable.Throw<string>(new Exception("oops"));


    }
  }
}