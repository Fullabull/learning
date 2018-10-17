using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using RxDemos;
using static System.Console;

namespace RxDemo.AdvancedSequenceOperators.ExceptionHandling
{
  public class MyTimer : IDisposable
  {
    private Stopwatch stopwatch = new Stopwatch();

    public MyTimer()
    {
      stopwatch.Start();
    }

    public void Dispose()
    {
      stopwatch.Stop();
      WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}msec");
    }
  }

  public class Demo
  {
    static void MainEH(string[] args)
    {
      var subj = new Subject<int>();
      var fallback = Observable.Range(1, 3);

      subj
        // .Catch(Observable.Empty<int>()) // catch-all
        //.Catch<int, InvalidOperationException>(ex => Observable.Return(-1))
        .Catch(fallback)
        .Finally(() => Console.WriteLine("This will be shown regardless")) // finally
        .Inspect("Catch");

      subj.OnNext(12);
      subj.OnError(new InvalidOperationException());
      subj.OnError(new Exception());
      subj.OnNext(34);

      Observable.Using( // should be moved
          () => new MyTimer(),
          timer => Observable.Interval(TimeSpan.FromSeconds(1)))
        .Take(3)
        .Inspect("Using");

      Thread.Sleep(4000);

      // onerrorresumenext will merge the two sequences
      var seq1 = new Subject<char>();
      var seq2 = new Subject<char>();

      seq1.OnErrorResumeNext(seq2).Inspect("OnErrorResumeNext");

      // merges two sequences whether or not an exception occurs
      seq1.OnNext('a', 'b', 'c')
        .OnCompleted() // required for merging the two sequences
        //.OnError<Exception, char>()
        ;

      seq2.OnNext('d', 'e', 'f');

      // retry re-subscribes to the source and tries again

      // try 4/3, 3/4
      // Retry yields # of attempts required to succeed
      SucceedAfter(3).Retry(5).Inspect("Retry");
    }

    private static IObservable<int> SucceedAfter(int attempts)
    {
      int count = 0;
      return Observable.Create<int>(o =>
      {
        Console.WriteLine((count > 0 ? "Ret" : "T") + "rying to do work");
        if (count++ < attempts)
        {
          Console.WriteLine("Failed");
          o.OnError();
        }
        else
        {
          Console.WriteLine("Succeeded");
          o.OnNext(count);
          o.OnCompleted();
        }
        return Disposable.Empty;
      });
    }
  }
}