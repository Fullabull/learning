using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filtering
{
    class EntryPoint
    {
        static void Main()
        {
            List<Person> people = new List<Person>()
            {
                new Buyer() { Age = 20 },
                new Buyer() { Age = 25 },
                new Buyer() { Age = 20 },
                new Supplier() { Age = 22 },
                new Supplier() { Age = 20 }
            };

            //---------------------------------------
            SeparatingLine();
            // 01. Filtering a collection by the type of its items
            var suppliers = from p in people
                            where p is Supplier
                            select p;

            var buyers = from p in people
                         where p is Buyer
                         select p;

            //---------------------------------------
            SeparatingLine();
            // 02. Filtering a collection by type and by additional filter (age) with query syntax
            var suppliersWithAgeFilter = from p in people
                                         let s = p as Supplier
                                         where s.Age == 20  // (p as Supplier).Age == 20
                                         select s;

            foreach (var item in suppliersWithAgeFilter)
            {
                Console.WriteLine(item.GetType().ToString());
            }

            //---------------------------------------
            SeparatingLine();
            // 03. Filtering a collection by type and by additional filter with method syntax
            var buyersWithAgeFilter = people.OfType<Buyer>().Where(b => b.Age == 20);
        }

        private static void SeparatingLine()
        {
            Console.WriteLine(new string('-', 40));
        }
    }

    internal class Person
    {

    }

    internal class Buyer : Person
    {
        public int Age { get; set; }
    }

    internal class Supplier : Person
    {
        public int Age { get; set; }
    }
}
