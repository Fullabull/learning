using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaExamples
{
    public class EntryPoint
    {
        static void Main()
        {
            string[] catNames = { "Lucky", "Bella", "Luna", "Oreo", "Simba", "Toby", "Loki", "Oscar" };
            List<int> numbers = new List<int>() { 5, 6, 3, 2, 1, 5, 6, 7, 8, 4, 234, 54, 14, 653, 3, 4, 5, 6, 7 };
            object[] mix = { 1, "string", 'd', new List<int>() { 1, 2, 3, 4 }, new List<int>() { 5, 2, 3, 4 }, "dd", 's', "Hello Kitty", 1, 2, 3, 4, };

            Console.WriteLine(new string('-', 40));
            // 1. Extract odd numbers with Lambda
            List<int> oddNumbers = numbers.Where(n => (n % 2) == 1).ToList();

            Console.WriteLine("The odd numbers are: " + string.Join(", ", oddNumbers));

            Console.WriteLine(new string('-', 40));
            // 2. Average Lambda
            double average = catNames.Average(c => c.Length);

            Console.WriteLine($"The average length of the cat names is: {average}");

            Console.WriteLine(new string('-', 40));
            // 3. Min, Max Lambda
            double minCatNameLength = catNames.Min(c => c.Length);
            double maxCatNameLength = catNames.Max(c => c.Length);
            double sumOfCatNameLengths = catNames.Sum(c => c.Length);

            Console.WriteLine($"Cat names length and sum - Min: {minCatNameLength}, Max: {maxCatNameLength}, Sum: {sumOfCatNameLengths}");

            Console.WriteLine(new string('-', 40));
            // 4. OfType Lambda
            var allInts = mix.OfType<int>();
            var allIntsLessThanThree = mix.OfType<int>().Where(i => i < 3);
            var containsIntLists = mix.OfType<List<int>>().ToList();

            Console.WriteLine("All numbers: " + string.Join(", ", allInts));
            Console.WriteLine("All numbers less than 3: " + string.Join(", ", allIntsLessThanThree));

            for (int i = 0; i < containsIntLists.Count; i++)
            {
                Console.WriteLine($"Int list[{i}]: " + string.Join(", ", containsIntLists[i]));
            }

            Console.WriteLine(new string('-', 40));
            // 5. Select vs Where
            List<Warrior> warriors = new List<Warrior>()
            {
                new Warrior() { Height = 100 },
                new Warrior() { Height = 80 },
                new Warrior() { Height = 100 },
                new Warrior() { Height = 70 },
            };

            List<int> heights = warriors.Where(wh => wh.Height == 100)
                                        .Select(wh => wh.Height)
                                        .ToList();

            Console.WriteLine("Heights: " + string.Join(", ", heights));

            Console.WriteLine(new string('-', 40));
            // 6. ForEach
            Console.WriteLine("Short ForEach heights");
            heights.ForEach(h => Console.WriteLine(h));

            Console.WriteLine("Short ForEach heights from Warriors");
            warriors.ForEach(wh => Console.WriteLine(wh.Height)); // can't do it with string.Join
        }
    }

    internal class Warrior
    {
        public int Height { get; set; }
    }
}
