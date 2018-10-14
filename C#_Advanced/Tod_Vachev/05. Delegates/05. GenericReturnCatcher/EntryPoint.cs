using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericReturnCatcher
{
    class EntryPoint
    {
        public delegate bool CheckLengthOfString(string message);
        public delegate int GetLength(string message);

        static void Main()
        {
            // Chain methods in a Delegate
            CheckLengthOfString d = LessThanFive;
            d += MoreThanFive;
            d += ExactlyFive;

            // Invoke the delegate, no output and no return values were caught
            d("Message");

            Console.WriteLine(new string('-', 40));
            List<bool> boolResults = CatchAllResults<bool>(d, "Message");

            Console.WriteLine(string.Join(", ", boolResults));

            GetLength p = x => x.Length;
            p += x => x.Length + 1;
            p += x => x.Length + 2;

            List<int> lengths = CatchAllResults<int>(p, "asd");

            Console.WriteLine(string.Join(", ", lengths));

        }

        public static List<T> CatchAllResults<T>(Delegate del, object parameter = null)
        {
            List<T> result = del.GetInvocationList()
                                .Select(d => (T)d.DynamicInvoke(parameter))
                                .ToList();

            return result;
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
