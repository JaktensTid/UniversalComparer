using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04._09._2017_UniversalComparer
{
    public class Person
    {
        public Person chief;
        public char? id1 { get; set; }
        public int? id2;
        public DateTime born;

        public Person(Person chief, char? id1, int? id2, DateTime born)
        {
            this.id1 = id1;
            this.id2 = id2;
            this.born = born;
            this.chief = chief;
        }

        public Person(Person chief, char? id1, int? id2) : this(chief, id1, id2, DateTime.Now) { }

        public Person(char? id1, int? id2) : this(null, id1, id2, DateTime.Now) { }
    }
}
