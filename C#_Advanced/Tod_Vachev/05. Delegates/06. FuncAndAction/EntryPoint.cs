using System;
using System.Collections.Generic;

namespace FuncAndAction
{
    public class EntryPoint
    {
        static void Main()
        {
            string[] names = { "Alice", "John", "Bobby", "Kyle", "Scott", "Tod", "Sharon", "Armin", "George" };
            
            // Refactoring our first example to use Func instead
            Func<string, bool> lessThanFive = x => x.Length < 5;
            Func<string, bool> moreThanFive = x => x.Length > 5;
            Func<string, bool> exactlyFive = x => x.Length == 5;

            List<string> namesLessThanFiveChars = ExtractStrings(names, lessThanFive);
            List<string> namesMoreThanFiveChars = ExtractStrings(names, moreThanFive);
            List<string> namesExactlyFiveChars = ExtractStrings(names, exactlyFive);

            Console.WriteLine("All names: " + string.Join(", ", names));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Names less than five chars: " + string.Join(", ", namesLessThanFiveChars));
            Console.WriteLine("Names more than five chars: " + string.Join(", ", namesMoreThanFiveChars));
            Console.WriteLine("Names exactly five chars: " + string.Join(", ", namesExactlyFiveChars));
            Console.WriteLine(new string('-', 40));
            
            // Action Delegate Example
            Action<string> printer = Print;
            printer += PrintTwice;
            printer += Print;

            printer("Message");
        }

        public static List<string> ExtractStrings(string[] array, Func<string, bool> filter)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < array.Length; i++)
            {
                if (filter(array[i]))
                {
                    result.Add(array[i]);
                }
            }

            return result;
        }

        public static void PrintTwice(string message)
        {
            Console.WriteLine(message + " " + message);
        }

        public static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
