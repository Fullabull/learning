using System;
using System.Reactive.Subjects;

namespace RxDemos.BehaviorSubjectDemo
{
  public class Scada
  {
    private BehaviorSubject<double> sensorValue;

    public IObservable<double> SensorValue => sensorValue;
  }

  public class Demo
  {
    static void MainBS(string[] args)
    {
      var sensorReading = new BehaviorSubject<double>(1.0);

      // 4) this prevents any value being posted
      //    not so with a BehaviorSubject
      sensorReading.OnCompleted();

      // 2) post a value
      sensorReading.OnNext(0.99);

      sensorReading.Subscribe(Console.WriteLine);

      // 1) prints a 1

      // 3) and another one
      sensorReading.OnNext(0.98);
    }
  }
}