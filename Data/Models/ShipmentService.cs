 using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ShipmentService
    {
        public int ShipmentId { get; set; }
        public Shipment? Shipment { get; set; }
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"Shipment ID: {ShipmentId} | Service: ID{ServiceId} {Service.Name} | " +
                   $" Notes: {Notes ?? "None"}";
        }

    }
}
