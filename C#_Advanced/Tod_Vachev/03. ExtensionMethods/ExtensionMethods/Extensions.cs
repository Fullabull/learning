using ExtensionMethodsProject;
using System;
using System.Drawing;

namespace Extensions
{
    public static class Extensions
    {
        // Extending Integer Array with a Sort method
        public static void Sort(this int[] array)
        {
            int temp = 0;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        temp = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }

        // Extending Integer Array with an overloaded Sort Method
        public static void Sort(this int[] array, bool reverse = false)
        {
            array.Sort();

            if (reverse)
            {
                array.Reverse();
            }
        }
        
        // Extending Integer Array with Reverse method
        public static void Reverse(this int[] array)
        {
            Array.Reverse(array);
        }


        // Extending the built in Point class
        public static Distance DistanceTo(this Point p1, Point p2)
        {
            Console.WriteLine($"The distance between P1 and P2 is " +
                $"\n{p2.X - p1.X} units in the X direction" +
                $"\n{p2.Y - p1.Y} units in the Y direction");

            return new Distance() { XDistance = p2.X - p1.X, YDistance = p2.Y - p1.Y };
        }
    }
}
