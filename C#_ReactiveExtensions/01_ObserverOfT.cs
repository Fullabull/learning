using System;
using System.Collections.Generic;

namespace RxDemos.ObserverOfT
{

  public class Demo : IObserver<float>
  {
    static void MainOOT(string[] args)
    {
      IObserver<float> o; // no need for nuget

      // OnNext* -> (OnError | OnCompleted)?

      Demo demo = new Demo();


    }

    public void OnNext(float value)
    {
      Console.WriteLine($"Market sent us {value}");
    }

    public void OnError(Exception error)
    {
      Console.WriteLine($"We failed due to {error}");
    }

    public void OnCompleted()
    {
      Console.WriteLine($"Market is closed");
    }
  }
}