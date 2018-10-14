using System;

namespace GenericMethods
{
    public class Person : IComparable<Person>
    {
        public int Age { get; set; }

        public int CompareTo(Person other)
        {
            if (this.Age < other.Age)
            {
                return -1;
            }
            else if (this.Age == other.Age)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
