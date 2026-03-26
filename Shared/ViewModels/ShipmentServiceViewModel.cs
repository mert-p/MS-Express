using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels
{
    public class ShipmentServiceViewModel
    {
        public int ShipmentId { get; set; }
        public string ServiceName { get; set; }
        public string DisplayExtraPrice { get; set; }   
        public string Notes { get; set; }

        public override string ToString()
        {
            return $"Shipment ID: {ShipmentId} | Service: {ServiceName} | " +
                   $"Extra Price: {DisplayExtraPrice} | Notes: {Notes}";
        }
    }
}
