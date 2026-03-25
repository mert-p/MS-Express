using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Courier:Person
    {
        public decimal Salary { get; set; }
        public List<Shipment> Shipments { get; set; }
        public override string ToString()
        {
            return $"{base.ToString()} Salary:{Salary:F2}";
        }
    }
}
