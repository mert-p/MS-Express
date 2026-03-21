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

        public ICollection<Shipment> Shipments { get; set; }
    }
}
