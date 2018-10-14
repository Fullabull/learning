namespace GenericMethods
{
    using System;

    class EntryPoint
    {
        static void Main()
        {
            Person p1 = new Person() { Age = 15 };
            Person p2 = new Person() { Age = 15 };

            Console.WriteLine(new string('-', 40));
            // 1. Using the AreEqual generic method to compare two objects of type Person, IComparable must be implemented in Person!
            Console.WriteLine(AreEqual(p1, p2));
            // Comparing Two Strings with the same method
            Console.WriteLine(AreEqual("Tod", "Vachev"));
            // Comparing Two Integers with the same method
            Console.WriteLine(AreEqual(5, 5));
            // Comparing Two Booleans with the same method
            Console.WriteLine(AreEqual(true, false));

            Console.WriteLine(new string('-', 40));
            // 2. Using the Sort Generic Method to Sort Arrays of Different Types
            int[] intArray = new int[] { 2, 1, 3 };
            char[] charArray = new char[] { 'a', 'f', 'c', 'd', 'b', 'z', 'g' };
            string[] stringArray = new string[] { "Tod", "Strawberry", "Cherry", "Coffee", "String", "Alphabet" };

            // Sorting an Integer Array
            Console.WriteLine(string.Join(", ", Sort(intArray)));
            // Sorting a Char Array
            Console.WriteLine(string.Join(", ", Sort(charArray)));
            // Sorting a String Array
            Console.WriteLine(string.Join(", ", Sort(stringArray)));
        }

        // Method to compare any two types that are of the same type and implement the IComparable<T> interface
        public static bool AreEqual<T>(T input1, T input2) where T : IComparable<T>
        {
            return input1.CompareTo(input2) == 0;
        }

        // Method to Sort an array of Any Type as long as its type implements the IComparable interface
        public static T[] Sort<T>(T[] array) where T : IComparable<T>
        {
            // Just a simple Selection Sort algorithm
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i].CompareTo(array[j]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }

            return array;
        }
    }



    
}
