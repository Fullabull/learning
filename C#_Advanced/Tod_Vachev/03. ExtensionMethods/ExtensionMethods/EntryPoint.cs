using System;
using System.Drawing;
using System.Collections.Generic;
using Extensions;

namespace ExtensionMethodsProject
{
    public static class EntryPoint
    {
        static void Main()
        {
            int[] array = { 2, 1, 3 };

            // 1. Sorting array with an Extension Method, the method is in Extensions.cs
            Console.WriteLine(new string('-', 40));
            array.Sort();
            Console.WriteLine("Sorted array: " + string.Join(", ", array));

            // 2. Sorting and Reversing an array with Extension Method, the method is in Extensions.cs
            Console.WriteLine(new string('-', 40));
            array.Sort(true);
            Console.WriteLine("Sorted and reversed array: " + string.Join(", ", array));

            // 3. Reversing an array with an Extension Method, the method is in Extensions.cs
            Console.WriteLine(new string('-', 40));
            array.Reverse();
            Console.WriteLine("Reversed Array" + string.Join(", ", array));

            // 4. Extending the built in Point class to find distance between two points.
            Console.WriteLine(new string('-', 40));
            Point p1 = new Point(20, 20);
            Point p2 = new Point(50, 60);

            // Distance between p1 and p2, saved in a Distance object and printed on the console
            Distance dist = p1.DistanceTo(p2);
        }
    }
}

