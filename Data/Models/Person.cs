using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public override string ToString()
        {
            return $"ID:{Id} Name:{FirstName} {LastName} Phone:{Phone}";
        }
    }
}
