using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using static System.Console;

namespace RxDemos.SequenceBasics.Filtering
{
  public class Demo
  {
    static void MainF(string[] args)
    {
      // operators are same as linq

      Observable.Range(0, 100)
        .Where(i => i % 9 == 0)
        .Subscribe(WriteLine);

      var values = Observable.Range(-10, 21);
      values
        .Select(x => x * x)
        .Distinct()
        .Subscribe(WriteLine);

      new List<int> {1, 1, 2, 2, 3, 3, 2, 2}
        .ToObservable()
        .DistinctUntilChanged()
        .IgnoreElements() // later, ignores actual elements, same as Where(_ => false)
        .Subscribe(
          WriteLine,
          () => WriteLine("Completed")
        );

      Observable.Range(1, 10)
        .Skip(3)
        .Take(4)
        .Subscribe(WriteLine);

      values.SkipWhile(x => x < 0)
        .TakeWhile(x => x < 6)
        .Subscribe(WriteLine);

      values.SkipLast(5).Subscribe(WriteLine); // caches all the values

      // skipuntil and takeuntil are weird

      // A and B are sequences
      // A will either skip or take the values until B produces a value
      var stockPrices = new Subject<float>();
      var optionPrices = new Subject<float>();



      // don't care about stock prices until option prices also available
      stockPrices.SkipUntil(optionPrices)
        .Subscribe(WriteLine);
      stockPrices.OnNext(1);
      stockPrices.OnNext(2);
      stockPrices.OnNext(3);
      optionPrices.OnNext(55);
      stockPrices.OnNext(4);
      stockPrices.OnNext(5);
      stockPrices.OnNext(6);
    }
  }
}