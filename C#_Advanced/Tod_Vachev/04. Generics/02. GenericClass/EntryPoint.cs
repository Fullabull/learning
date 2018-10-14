using _02.GenericClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericClass
{
    class EntryPoint
    {
        static void Main()
        {
            //
            Console.WriteLine(new string('-', 40));
            //

            MyList<int> firstIntList = new MyList<int>();
            MyList<int> secondIntList = new MyList<int>();
            
            // Populating the two lists with numbers
            for (int i = 0; i < 10; i++)
            {
                firstIntList.Add(1);
                secondIntList.Add(2);
            }
            
            // Checking if Capacity is correctly doubling
            Console.WriteLine($"The list has {firstIntList.Count} items in it and its capacity is {firstIntList.Capacity}.");

            // Adding two lists
            MyList<int> thirdIntList = firstIntList + secondIntList;

            Console.WriteLine($"All items in the Integer Lists:" +
                $"\nList 1 - {firstIntList.ToString()}" +
                $"\nList 2 - {secondIntList.ToString()}" +
                $"\nList 3 - {thirdIntList.ToString()}");
            
            //
            Console.WriteLine(new string('-', 40));
            //

            MyList<string> firstStringList = new MyList<string>();
            MyList<string> secondStringList = new MyList<string>();

            // Populate the two string lists
            for (int i = 0; i < 10; i++)
            {
                firstStringList.Add("a");
                secondStringList.Add("b");
            }

            // Add the two string lists
            MyList<string> thirdStringList = firstStringList + secondStringList;
            
            Console.WriteLine($"All items from the String lists:" +
                $"\nList 1 - {firstStringList.ToString()}" +
                $"\nList 2 - {secondStringList.ToString()}" +
                $"\nList 3 - {thirdStringList.ToString()}");
        }
    }
}
