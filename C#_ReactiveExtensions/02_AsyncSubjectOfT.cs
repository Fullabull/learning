using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace RxDemos.AsyncSubjectDemo
{
  public class Demo
  {
    static void MainAS(string[] args)
    {
      Task<int> t = Task<int>.Factory.StartNew(() => 42);
      int value = t.Result;

      // AsyncSubject always stores the last value, and only
      // gives it up on OnCompleted

      AsyncSubject<double> sensor = new AsyncSubject<double>();
      sensor.OnNext(10);
      sensor.Subscribe(Console.WriteLine);
      sensor.OnNext(20);
      sensor.OnCompleted();

      // implicit contact - sequence ends in either
      // OnCompleted or OnError
      sensor.OnNext(30); // does nothing
    }
  }
}