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
        public decimal ExtraPrice { get; set; }
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"Shipment ID: {ShipmentId} | Service: ID{ServiceId} {Service.Name} | " +
                   $"Extra Price: {ExtraPrice:C} | Notes: {Notes ?? "None"}";
        }

    }
}
