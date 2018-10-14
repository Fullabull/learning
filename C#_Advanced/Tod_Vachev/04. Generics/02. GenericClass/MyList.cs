using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.GenericClass
{
    public class MyList<T>
    {
        private const int DEFAULT_SIZE = 2;

        private T[] items;

        private int count;
        private int capacity;

        public int Count
        {
            get
            {
                return this.count;
            }
            private set
            {
                this.count = value;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            private set
            {
                this.capacity = value;
            }
        }

        public T this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                this.items[index] = value;
            }
        }

        public MyList()
        {
            this.items = new T[DEFAULT_SIZE];
            this.capacity = DEFAULT_SIZE;
            this.count = 0;
        }

        public void Add(T item)
        {
            // When the Count (items in the array) is the same as the Capacity of the array, the size of the array must be increased (doubled)
            if (this.capacity == this.count)
            {
                T[] clone = (T[])items.Clone();

                // Double capacity and reinitialize items array
                this.capacity *= 2;
                this.items = new T[this.capacity];

                // Restore the items in the new bigger array
                Array.Copy(clone, 0, this.items, 0, clone.Length);
            }

            this.items[this.Count] = item;
            this.Count++;
        }

        public static MyList<T> operator +(MyList<T> list1, MyList<T> list2)
        {
            MyList<T> result = new MyList<T>();

            if (list1.count != list2.count)
            {
                // We can only add lists that are of the same size
                throw new InvalidOperationException("Lists are of different sizes!");
            }
            else
            {
                for (int i = 0; i < list1.count; i++)
                {
                    // list items need to be typecasted to dynamic
                    result.Add((dynamic)list1[i] + (dynamic)list2[i]);
                }
            }

            return result;
        }

        public override string ToString()
        {
            string tempString = string.Empty;

            for (int i = 0; i < this.count; i++)
            {
                if (i < this.count - 1)
                {
                    tempString += this.items[i] + ", ";
                }
                else
                {
                    tempString += this.items[i];
                }
            }

            return tempString;

            // Easy, but we also get all of the "empty" items
            //return string.Join(" ", this.items);
        }
    }
}
