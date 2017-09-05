using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04._09._2017_UniversalComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person(new Person(null, 'a', 2), null, 1);
            Person person2 = new Person(new Person(null, 'b', 2), 'a', 2);
            Person person3 = new Person(new Person(null, 'c', 2), 'b', 3);
            Person person4 = new Person(new Person(null, 'd', 2), 'c', 4);

            UniversalComparer uc = new UniversalComparer("id1", false);

            Person[] persons = { person1, person2, person3, person4 };
            Array.Sort(persons, uc);
            foreach (var person in persons)
            {
                Console.WriteLine(person.id1 + "-" + person.id2);
            }

            Console.ReadKey();
        }
    }
}
