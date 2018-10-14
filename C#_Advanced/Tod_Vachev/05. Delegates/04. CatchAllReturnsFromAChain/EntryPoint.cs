using System;
using System.Collections.Generic;
using System.Linq;

namespace CatchAllReturns
{
    class EntryPoint
    {
        public delegate bool CheckLengthOfString(string message);

        static void Main()
        {
            // Chain methods in a Delegate
            CheckLengthOfString d = LessThanFive;
            d += MoreThanFive;
            d += ExactlyFive;

            // Invoke the delegate, no output and no return values were caught
            d("Message");

            Console.WriteLine(new string('-', 40));
            // Catch all results with a ForEach loop
            List<bool> results = new List<bool>();

            foreach (var del in d.GetInvocationList())
            {
                results.Add((bool)del.DynamicInvoke("Message"));
            }

            Console.WriteLine("Foreach loop results: " + string.Join(", ", results));

            // Catch all results with a Lambda Expression
            Console.WriteLine(new string('-', 40));
            List<bool> lambdaResults = d.GetInvocationList()
                                        .Select(del => (bool)del.DynamicInvoke("Message"))
                                        .ToList();

            Console.WriteLine("LINQ Lambda Results: " + string.Join(", ", lambdaResults));
        }

        public static bool LessThanFive(string item)
        {
            return item.Length < 5;
        }

        public static bool MoreThanFive(string item)
        {
            return item.Length > 5;
        }

        public static bool ExactlyFive(string item)
        {
            return item.Length == 5;
        }
    }
}
