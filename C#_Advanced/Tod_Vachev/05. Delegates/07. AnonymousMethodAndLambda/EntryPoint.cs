using System;
using System.Collections.Generic;

namespace AnonymousMethodAndLambda
{
    public class EntryPoint
    {
        static void Main()
        {
            Func<int, int, bool> isLessThanFive = (int i, int j) => i < 5 + j;

            Action<int, int> printSum = (int i, int j) => Console.WriteLine($"The sum of {i} + {j} is {i + j}");

            Action<int, int> sumOfTwoNumbers = (int i, int j) =>
            {
                Console.WriteLine("The i number is: " + i);
                Console.WriteLine("The j number is: " + j);
                Console.WriteLine("And their sum: " + i + j);
            };

            Console.WriteLine("Is 4 less than 5? " + isLessThanFive(4, 5));
            printSum(3, 2);
            sumOfTwoNumbers(5, 6);

            // Anonymous Method Example
            Console.WriteLine(new string('-', 40));

            string[] names = { "Alice", "John", "Bobby", "Kyle", "Scott", "Tod", "Sharon", "Armin", "George" };

            Func<string[], Func<string, bool>, List<string>> extractStrings = (arr, filter) =>
            {
                List<string> result = new List<string>();

                for (int i = 0; i < arr.Length; i++)
                {
                    if (filter(arr[i]))
                    {
                        result.Add(arr[i]);
                    }
                }

                return result;
            };
            Func<string, bool> lessThanFive = x => x.Length < 5;

            List<string> namesLessThanFiveChars = extractStrings(names, lessThanFive);

            Console.WriteLine(string.Join(", ", namesLessThanFiveChars));
        }

    }
}
