using System;
using System.Reactive.Subjects;
using System.Threading;

namespace RxDemos.ReplaySubjectOfT
{


  public class Demo : IObserver<float>
  {

    public Demo()
    {
                   // vvvv start with Subject
      var bufferSize = 2; // change to 3
      var timeWindow = TimeSpan.FromMilliseconds(500);
      //var market = new ReplaySubject<float>(bufferSize);
      var market = new ReplaySubject<float>(timeWindow);
      market.OnNext(123);
      Thread.Sleep(200);
      market.OnNext(234);
      Thread.Sleep(200);
      market.OnNext(456);
      Thread.Sleep(200);
      market.Subscribe(this);
      market.OnNext(567);
    }
    static void MainRSOT(string[] args)
    {
      new Demo();
    }

    public void OnNext(float value)
    {
      Console.WriteLine($"I got the value {value}");
    }

    public void OnError(Exception error)
    {
      throw new NotImplementedException();
    }

    public void OnCompleted()
    {
      throw new NotImplementedException();
    }
  }
}