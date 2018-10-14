using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Reactive;
using System.Runtime.CompilerServices;
//using System.Reactive.Core;
using System.Reactive.Disposables;
//using System.Reactive.Interfaces;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;

using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Annotations;
using Timer = System.Timers.Timer;

namespace ConsoleApp1
{
    public static class MyExtensions
    {
        public static IObservable<string> Inspect (this IObservable<string> observer)
        {
            observer.Subscribe(Console.WriteLine, x => Console.WriteLine($"My extension supports: {x.ToString()}"));
            return observer;  
        }
    }

    class Program_BACKUP_01
    {
        public static void Main_BACKUP_01(string[] args)
        {
            //var t = Observable.Timer(TimeSpan.FromSeconds(1));
            //t.Subscribe(x => Console.WriteLine($"{x.ToString()}"));
            //Console.ReadLine();
            var subject = new Subject<int>();
            subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Subject completed"));
            var any = subject.Any();
            any.Subscribe(b => Console.WriteLine("The subject has any values? {0}", b));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnCompleted();
        }
    }
}