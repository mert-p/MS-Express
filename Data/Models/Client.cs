using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Client:Person
    {
        public string  Email { get; set; }
        public string Address { get; set; }
        public List<Shipment>? SentShipments { get; set; }
        public List<Shipment>? ReceivedShipments { get; set; }
        public override string ToString()
        {
            return $"{base.ToString()} Email:{Email} Address:{Address}";
        }
    }
}
