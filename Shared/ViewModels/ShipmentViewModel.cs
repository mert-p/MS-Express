using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels
{
    public class ShipmentViewModel
    {
        public int Id { get; set; }
        public string SenderFullName { get; set; }
        public string ReceiverFullName { get; set; }
        public string CourierFullName { get; set; }
        public string Weight { get; set; }
        public string DisplayPrice { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public override string ToString()
        {
            return $"Shipment ID: {Id} | Sender: {SenderFullName} | " +
                   $"Receiver: {ReceiverFullName} | " +
                   $"Courier: {CourierFullName} | " +
                   $"Weight: {Weight} | Price: {DisplayPrice} | " +
                   $"Type: {Type} | Date: {Date} | Status: {Status}";
        }
    }
}
