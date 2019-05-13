using System;
using System.Collections.Generic;

namespace RESTfullAPIRoComputer
{
    public partial class Person
    {
        public Person()
        {
            PersonData = new HashSet<PersonData>();
        }

        public string Email { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }

        public virtual ICollection<PersonData> PersonData { get; set; }
    }
}