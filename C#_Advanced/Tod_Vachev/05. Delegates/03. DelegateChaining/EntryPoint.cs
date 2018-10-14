using System;

namespace DelegateChaining
{
    class EntryPoint
    {
        public delegate void Printer(string message);

        static void Main()
        {
            // Chain methods in a Delegate
            Printer d = Print;
            d += Print;
            d += PrintTwice;
            d += Print;

            // Invoke the delegate
            d("Message");

            // Get the list of methods in the chain
            foreach (var del in d.GetInvocationList())
            {
                Console.WriteLine(del.Method);
            }
            
            // Extract the chain of methods in a Delegate Array.
            Delegate[] dels = d.GetInvocationList();

            Console.WriteLine($"There are {dels.Length} methods in the chain.");
            Console.WriteLine("The third delegate in the chain is: " + dels[2].Method);
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
