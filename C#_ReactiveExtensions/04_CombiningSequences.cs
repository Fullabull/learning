using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace RxDemos.AdvancedSequenceOperators.CombiningSequences
{
  public class Demo
  {
    static void MainCS(string[] args)
    {
      // CombineLatest

      var mechanical = new BehaviorSubject<bool>(true);
      var electrical = new BehaviorSubject<bool>(true);
      var electronic = new BehaviorSubject<bool>(true);

      mechanical.Inspect("mechanical");
      electrical.Inspect("electrical");
      electronic.Inspect("electronic");

      Observable.CombineLatest(mechanical, electrical, electronic)
        .Select(values => values.All(x => x))
        .Inspect("Is the system OK?");

      mechanical.OnNext(false);

      var digits = Observable.Range(0, 10);
      var letters = Observable.Range(0, 10).Select(x => (char)('A' + x));

      letters.Zip(digits, (letter, digit) => $"{letter}{digit}")
        .Inspect("Zip");

      // And returns Pattern<T1, T2> whose properties are internal
      var punctuation = "!@#$%^&*()".ToCharArray().ToObservable();
      Observable.When( // Plan<> and all that
          digits
            .And(letters)
            .And(punctuation)
            .Then((digit, letter, symbol) => $"{digit}{letter}{symbol}")
        )
        .Inspect("And-Then-When");
    }

    static void CombiningSequences(string[] args)
    {
      // Concat merges all the sequences into one
      var s1 = Observable.Range(1, 3);
      var s2 = Observable.Range(4, 3);
      s2.Concat(s1).Inspect("Concat");

      // Repeat repeats the sequence as often as is necessary
      s1.Repeat(3).Inspect("Repeat");

      s1.StartWith(2, 1, 0).Inspect("StartWith");

      // Amb(iguous)
      // will return a value from the sequence that first produces a value
      // will ignore values from all other sequences

      var seq1 = new Subject<int>();
      var seq2 = new Subject<int>();
      var seq3 = new Subject<int>();
      seq1.Amb(seq2).Amb(seq3).Inspect("Amb");
      seq1.OnNext(1); // comment this out
      seq2.OnNext(2);
      seq3.OnNext(3);
      seq1.OnNext(1);
      seq2.OnNext(2);
      seq3.OnNext(3);
      seq1.OnCompleted();
      seq2.OnCompleted();
      seq3.OnCompleted();

      // Merge pairs up values from multiple sequences
      var foo = new Subject<long>();
      var bar = new Subject<long>();
      var baz = Observable.Interval(TimeSpan.FromMilliseconds(500)).Take(5);

      foo.Merge(bar).Merge(baz).Inspect("Merge");

      foo.OnNext(100);
      Thread.Sleep(1000);
      bar.OnNext(10);
      Thread.Sleep(1000);
      foo.OnNext(1000);
      Thread.Sleep(1000);
    }
  }
}