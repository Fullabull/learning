using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxDemos.SequenceBasics.Aggregation
{
  public class Demo
  {
    static void MainA(string[] args)
    {
      var values = Observable.Range(1, 5);
      values.Inspect("values");
      values.Count().Inspect("count");

      var intSubj = new Subject<int>();
      intSubj.Inspect("intSubj");
      intSubj.Min().Inspect("min"); // not a running minimum!
      intSubj.Sum().Inspect("sum"); // requires all values
      intSubj.Average().Inspect("avg"); // double!

      intSubj.OnNext(2);
      intSubj.OnNext(4);
      intSubj.OnNext(1);
      intSubj.OnCompleted();

      // first, last, single
      var replay = new ReplaySubject<int>();

      // later
      replay.OnNext(-1); // doesn't complete unless OnCompleted
      replay.OnNext(2);

      replay.OnCompleted();

      replay.FirstAsync(i => i > 0).Inspect("FirstAsync"); // doesn't require completion

      replay.FirstOrDefaultAsync(i => i > 10) // yields 0 because no element matches
        .Inspect("FirstOrDefaultAsync"); // doesn't require completion

      replay.SingleAsync().Inspect("SingleAsync"); // try commenting out one of OnNext
      // requires completion! try commenting out OnCompleted -> no output

      //replay.SingleOrDefaultAsync()

      // Sum is always the final value, how about a running sum?
      var subj = new Subject<double>();
      int power = 1;

      subj.Aggregate(0.0, (p, c) => p + Math.Pow(c, power++)).Inspect("poly");
      subj.OnNext(1,2,4).OnCompleted(); // 1^1 + 2^2 + 4^3

      // running sum? no problem
      var subj2 = new Subject<double>();
      subj2.Scan(0.0, (p, c) => p + c).Inspect("scan"); // same as Aggregate().TakeLast()
      subj2.OnNext(1, 2, 3); //.OnCompleted();
                             // OnCompleted doesn't really matter anymore
    }
  }
}