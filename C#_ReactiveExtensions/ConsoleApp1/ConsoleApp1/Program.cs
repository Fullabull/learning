using System;
using System.ComponentModel;
using System.Reactive.Subjects;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    class Program
    {

        public static void Main(string[] args)
        {
            var sensor = new Subject<float>();
            using (sensor.Subscribe(Console.WriteLine))
            {
                sensor.OnNext(1);
                sensor.OnNext(2);
                sensor.OnNext(3);
            }
        }
    }
}


