using System;
using System.Linq;
using _04._09._2017_UniversalComparer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAscDesc()
        {
            Person person1 = new Person('a', 4);
            Person person2 = new Person('b', 3);
            Person person3 = new Person('c', 2);
            Person person4 = new Person('d', 1);
            Person person5 = new Person('e', 11);
            UniversalComparer uc = new UniversalComparer("id1 desc, id2", true);

            Person[] persons = { person1, person2, person3, person4, person5 };
            Array.Sort(persons, uc);

            Assert.AreEqual(persons[0].id1, 'e');
            Assert.AreEqual(persons.Last().id1, 'a');
        }

        [TestMethod]
        public void TestNullable()
        {
            Person person1 = new Person(new Person(null, 'a', 2), null, 1);
            Person person2 = new Person(new Person(null, 'b', 2), 'a', 2);
            Person person3 = new Person(new Person(null, 'c', 2), 'b', 3);
            Person person4 = new Person(new Person(null, 'd', 2), 'c', 4);

            UniversalComparer uc = new UniversalComparer("id1 asc", true);

            Person[] persons = { person1, person2, person3, person4 };
            Array.Sort(persons, uc);

            Assert.AreEqual(persons[0].id1, null);
        }

        [TestMethod]
        public void TestInnerProperty()
        {
            Person person1 = new Person(new Person(null, 'a', 2), null, 1);
            Person person2 = new Person(new Person(null, 'b', 2), 'a', 2);
            Person person3 = new Person(new Person(null, 'c', 2), 'b', 3);
            Person person4 = new Person(new Person(null, 'd', 2), 'c', 4);

            UniversalComparer uc = new UniversalComparer("born.Millisecond");

            Person[] persons = { person1, person2, person3, person4 };
            Array.Sort(persons, uc);

            int smallest = person1.born.Millisecond;
            foreach (var person in persons)
            {
                if (person.born.Millisecond < smallest) smallest = person.born.Millisecond;
            }
        }
    }
}
