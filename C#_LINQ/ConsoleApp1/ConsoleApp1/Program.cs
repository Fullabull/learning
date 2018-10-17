﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ConsoleApp1
{
    public class Person
    {
        public string Name;
        public int Value;
        public int Change;
    }

    public class Market
    {
        public BindingList<float> Prices = new BindingList<float>();

        public void AddPrice(float price)
        {
            Prices.Add(price);
            //PriceAdded?.Invoke(this, price);
        }
        //public event EventHandler<float> PriceAdded;
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Market market = new Market();
            //market.PriceAdded += (sender, f) => { Console.WriteLine($"we got --> {f}"); };
            market.Prices.ListChanged += (sender, eventArgs) =>
            {
                if (eventArgs.ListChangedType == ListChangedType.ItemAdded)
                {
                    float f = ((BindingList<float>) sender)[eventArgs.NewIndex];
                    Console.WriteLine($"we got --> {f}");
                }
            };

            market.AddPrice(54321);
        }

        public static void Main_06(string[] args)
        {
            Market market = new Market();
            //market.PriceAdded += (sender, f) => { Console.WriteLine($"we got --> {f}"); };
            market.Prices.ListChanged += (sender, f) => { Console.WriteLine($"we got --> {f}"); };

            market.AddPrice(54321);
        }

        public static void Main_05(string[] args)
        {
            var fontsizes = Enumerable.Range(1, 10).Select(x => (x * 10) + " pt");
            foreach (string f in fontsizes)
            {
                Console.WriteLine(f);
            }

            var alphabet = Enumerable.Range(0, 26).Select(c => ((char) (c + 'a')).ToString());
            foreach (var x in alphabet)
            {
                Console.WriteLine(x);
            }
        }

        public static void Main_04(string[] args)
        {
            Random random = new Random();
            var randoms =
                Enumerable.Range(1, 10).Select(_ => random.Next(1, 100))
                    .ToList(); // Select(_ => means ignore the numbers generated by Range(1,10) - just execute range.Next 10 times!
            foreach (int x in randoms)
            {
                Console.WriteLine(x);
            }
        }

        public static void Main_03(string[] args)
        {
            var value1 = Enumerable.Range(1, 10).Where(n => (n % 2) == 0);
            var value2 = from n in Enumerable.Range(1, 10) where n % 2 == 1 select n;
            var value = value1.Concat(value2).ToList();
            foreach (int x in value)
            {
                Console.WriteLine(x);
            }
        }

        public static void Main_02(string[] args)
        {
            var value = Enumerable.Repeat(5, 50);
            foreach (int x in value)
            {
                Console.WriteLine(x);
            }
        }

        public static void Main_01(string[] args)
        {
            List<Person> l1 = new List<Person>()
            {
                new Person() {Name = "Andy", Value = 0, Change = 0},
                new Person() {Name = "Billy", Value = -10, Change = 0},
                new Person() {Name = "Charlie", Value = 0, Change = 0}
            };
            List<Person> l2 = new List<Person>()
            {
                new Person() {Name = "Billy", Value = 15, Change = 0},
                new Person() {Name = "Dillon", Value = 10, Change = 0},
                new Person() {Name = "Andy", Value = 05, Change = 0},
                new Person() {Name = "Charlie", Value = 20, Change = 0}
            };

            List<Person> list = l1.Concat(l2).ToList();
            List<Person> list3 = l1.Concat(l2)
                .ToLookup(p => p.Name)
                .Select(g => g.Aggregate(
                    (p1, p2) => new Person
                    {
                        Name = p1.Name,
                        Value = p1.Value,
                        Change = p2.Value - p1.Value
                    })).ToList();
            foreach (var x in list3)
            {
                Console.WriteLine($"{x.Name}, {x.Value}, {x.Change}");
            }
        }
    }
}