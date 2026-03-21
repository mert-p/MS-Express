using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Service
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public decimal Price { get; set; }    
        public List<ShipmentService> ShipmentServices { get; set; }
    }
}
