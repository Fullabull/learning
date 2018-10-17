using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using static System.Console;

namespace RxDemos.SequenceBasics.Inspection
{
  public class Demo
  {
    static void MainI(string[] args)
    {
      // Any - true or false depending on whether the sequence has any elements
      // this means we have to wait for the sequence to complete before making the
      // termination

      var subject = new Subject<int>();

      subject.Any(x => x > 1) // later add predicate
        .Subscribe(
          x => WriteLine($"Did we get any values? {x}")
        );

      subject.OnNext(1); // later
      WriteLine("Let's post another one");
      subject.OnNext(2);

      subject.OnCompleted();

      var values = new List<int> {1, 2, 3, 4, 5};
      values.ToObservable()
        .All(x => x > 0)
        .Subscribe(WriteLine);

      var subj2 = new Subject<string>();

      subj2.Contains("bar")
        .Subscribe(x => Console.WriteLine($"Does subject contain 'bar'? {x}"));

      subj2.OnNext("foo");
      //subj2.OnNext("bar");
      subj2.OnNext("baz");
      //subj2.OnCompleted(); // commenting out causes Does... to never be output

      var subj3 = new Subject<float>();
      subj3.DefaultIfEmpty(-99.9f) // note strict type adherence!
        .Subscribe(WriteLine);

      // later add
      subj3.OnNext(100.0f); // replaces -99.9f
      subj3.OnNext(101.0f);

      subj3.OnCompleted();

      var numbers = Observable.Range(1, 10);
      numbers.ElementAt(5) // try changing to 15
        .Subscribe(
          x => Console.WriteLine($"The element at position 5 is {x}"),
          ex => Console.WriteLine($"Could not get element because {ex.Message}")
        );

      // comparison of two sequences

      var seq1 = new Subject<int>();
      var seq2 = new Subject<int>();

      seq1.SequenceEqual(seq2)
        .Subscribe(x => Console.WriteLine($"Are sequences equal? {x}"));

      seq1.Subscribe(x => Console.WriteLine($"seq1 produces {x}"));
      seq2.Subscribe(x => Console.WriteLine($"seq2 produces {x}"));

      seq1.OnNext(1);
      seq1.OnNext(2);

      seq2.OnNext(1);
      seq2.OnNext(2); // try changing to 3

      seq1.OnCompleted();
      seq2.OnCompleted(); // both sequences need to complete for result to be available
    }
  }
}