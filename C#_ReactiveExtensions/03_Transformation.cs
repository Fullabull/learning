using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using static System.Console;

namespace RxDemos.SequenceBasics.Transformation
{
  public class Demo
  {
    static void MainT(string[] args)
    {
      // select
      // cast + oftype
      // timestamp, timeinterval
      // materialize/dematerialize
      // selectmany

      var numbers = Observable.Range(1, 10);

      numbers.Select(x => x * x).Inspect("squares");

      var subj = new Subject<object>();

      subj.OfType<float>().Inspect("OfType"); // just filters out the right values
      subj.Cast<float>().Inspect("Cast");     // tries to cast every value to this type

      subj.OnNext(1.0f);
      subj.OnNext(2); // int
      subj.OnNext(3.0); // double

      // timestamp just posts the creation time
//      var seq = Observable.Interval(TimeSpan.FromSeconds(1));
//      seq.Timestamp().Inspect("Timestamp");
//      seq.TimeInterval().Inspect("TimeInterval");

      //ReadKey();

      var seq2 = Observable.Range(0, 4);
      // Notification<int>
      seq2.Materialize()
        //.Select(n => n.)
        //.Select(n => n.Value)
        .Dematerialize() // later
        .Inspect("materialize");

      // SelectMany

      // 1 1 2 1 2 3
      Observable.Range(1, 3) // try changing to 4
        .SelectMany(x => Observable.Range(1, x))
        .Inspect("gen");

      // this enforces the order
      Observable.Range(1, 4, ImmediateScheduler.Instance) // try changing to 4
        .SelectMany(x => Observable.Range(1, x, ImmediateScheduler.Instance))
        .Inspect("gen");
    }
  }
}