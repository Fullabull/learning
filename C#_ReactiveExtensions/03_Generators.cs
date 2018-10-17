using System;
using System.Reactive.Linq;

namespace RxDemos.SequenceBasics.Generators
{
  public class Demo
  {
    // Timer
    static void MainG(string[] args)
    {
      // publishes only one value, then completes
//      var timer = Observable.Timer(TimeSpan.FromSeconds(2));
//      timer.Subscribe(Console.WriteLine,
//        () => Console.WriteLine("And we are done"));
//      Console.ReadLine();


      // publishes values starting with
//      var t = TimeSpan.FromMilliseconds(500);
//      //var interval = Observable.Interval(t);
//      var interval = Observable.Timer(
//        //t, // dueTime
//        TimeSpan.Zero, // no time delay in this set-up
//        t // period
//      );
//      using (interval.Subscribe(Console.WriteLine,
//        () => Console.WriteLine("we're done")))
//      {
//        Console.ReadKey();
//      }

      // merge timing functionality and sequence generator functionality

      var dueTime = TimeSpan.FromMilliseconds(2000);
      var period = TimeSpan.FromMilliseconds(500);
      var sequence = Observable.Generate(
        0,
        i => i < 10000,
        i => i * i + 1,
        i => Math.Sqrt(i),
        i => i == 0 ? dueTime : period
      ); // infinite sequences
      using (sequence.Subscribe(Console.WriteLine))
      {
        Console.ReadKey();
      }
    }

    // observable.range
    static void MainCo(string[] args)
    {
//      var tenToTwenty = Observable.Range(10, 11);
//      tenToTwenty.Subscribe(Console.WriteLine,
//        () => Console.WriteLine("Completed"));

      var generated = Observable.Generate(1, // first value yielded
          value => value < 100, // constrain generation (limiting value)
          value => value*value + 1, // iteration step
          value => $"[{value}]"); // the Select() on the end

      generated.Subscribe(Console.WriteLine);

      // Observable.Interval pushes incremental time values
      var interval = Observable.Interval(TimeSpan.FromMilliseconds(500));
      using (interval.Subscribe(Console.WriteLine))
      {
        Console.ReadLine();
      }


    }
  }
}