using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<Order> Orders { get; set; }
    }
}
