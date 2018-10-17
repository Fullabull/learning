using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using Console = System.Console;

namespace RxDemos.AdvancedSequenceOperators.TimeShiftedSequences
{
  public class Demo
  {
    static void MainTSS(string[] args)
    {
      // Buffer
      Observable.Range(1, 100)
        .Buffer(5 /*, 3*/) // elements to skip on subsequent operation
        // skip value can exceed buffer size, causing
        // elements to be dropped
        .Subscribe(x =>
          Console.WriteLine($"Got a group of {x.Count} elements: " +
                            string.Join(",", x))
        );

      // Delay - simply time-shifts the sequence
      var source = Observable.Interval(TimeSpan.FromSeconds(1))
        .Take(3);
      var delay = source.Delay(TimeSpan.FromSeconds(2));
      source.Timestamp().Inspect("source");
      delay.Timestamp().Inspect("delay");

      Thread.Sleep(10000);

      // Sample - takes the last value that was available in a given timespan
      var samples = Observable.Interval(TimeSpan.FromSeconds(0.5))
        .Take(20)
        .Sample(TimeSpan.FromSeconds(1.75));
      samples.Inspect("Sample");
      samples.ToTask().Wait();

      // Throttle - just like Sample, but the wait window is reset
      // needs to run in Debug mode (external window)
      var subj = new Subject<string>();

      subj // also Timeout, which causes an exception if nothing happens in X seconds
        .Timeout(TimeSpan.FromSeconds(3), // this argument alone would throw
          Observable.Empty<string>()) // this prevents throwing
        //.Throttle(TimeSpan.FromSeconds(1))
        .TimeInterval()
        .Inspect("Throttle");

      string input = string.Empty;
      ConsoleKeyInfo c;
      while ((c = Console.ReadKey()).Key != ConsoleKey.Escape)
      {
        if (char.IsLetterOrDigit(c.KeyChar))
        {
          input += c.KeyChar;
          subj.OnNext(input);
        }
        else if (c.Key == ConsoleKey.Backspace && input.Length > 0)
        {
          input = input.Substring(0, input.Length - 1);
        }
      }
    }
  }
}