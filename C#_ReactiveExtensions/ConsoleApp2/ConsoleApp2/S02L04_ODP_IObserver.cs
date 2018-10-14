using System;

namespace ConsoleApp2
{
    public class S02L04_ODP_IObserver
    {

        internal class Program : IObserver<float>
        {
            public static void MainX(string[] args)
            {
                // OnNext* --> ( OnCompleted | OnError )? -->
            }

            public void OnNext(float value)
            {
                Console.WriteLine($"Market gave us {value}");
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
}