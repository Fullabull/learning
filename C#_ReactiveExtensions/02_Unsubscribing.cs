using System;
using System.Reactive.Subjects;

namespace RxDemos.LifetimeMgmt.UnSubscribing
{
  public class Demo
  {
    static void MainUS(string[] args)
    {
      var sensor = new Subject<float>();

      // stick it in a using

      using (var subscription = sensor.Subscribe(Console.WriteLine))
      {
        sensor.OnNext(1);
        sensor.OnNext(2);
        sensor.OnNext(3);
      }

      //subscription.Dispose();

      //sensor.OnNext(4);
      //sensor.OnNext(5);
    }
  }
}