using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Courier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }   
        public decimal Salary { get; set; }
        public List<Shipment> Shipments { get; set; }
        public override string ToString()
        {
            return $"ID:{Id}, Name:{Name}\nPhone number:{Phone}\nSalary:{Salary:F2}";
        }
    }
}
