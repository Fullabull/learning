using System;
using System.Collections.Generic;

namespace Delegates
{
    public class EntryPoint
    {
        public delegate bool NamesFilter(string item);

        static void Main()
        {
            string[] names = { "Alice", "John", "Bobby", "Kyle", "Scott", "Tod", "Sharon", "Armin", "George" };

            List<string> namesLessThanFiveChars = ExtractStrings(names, LessThanFive);
            List<string> namesMoreThanFiveChars = ExtractStrings(names, MoreThanFive);
            List<string> namesExactlyFiveChars = ExtractStrings(names, ExactlyFive);

            Console.WriteLine("All names: " + string.Join(", ", names));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("Names less than five chars: " + string.Join(", ", namesLessThanFiveChars));
            Console.WriteLine("Names more than five chars: " + string.Join(", ", namesMoreThanFiveChars));
            Console.WriteLine("Names exactly five chars: " + string.Join(", ", namesExactlyFiveChars));
            Console.WriteLine(new string('-', 40));
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

        public static List<string> ExtractStrings(string[] array, NamesFilter filter)
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
    }
}
