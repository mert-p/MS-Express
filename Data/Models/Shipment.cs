using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public Client? ClientSender { get; set; }
        public int ReceiverId { get; set; }
        public Client? ClientReceiver { get; set; }
        public int CourierId { get; set; }
        public Courier? Courier { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }  
        public string Status { get; set; }  
        public List<ShipmentService>? ShipmentServices { get; set; }
        public override string ToString()
        {
            return $"Shipment ID: {Id} | Sender: {ClientSender?.FirstName} {ClientSender?.LastName} | " +
                   $"Receiver: {ClientReceiver?.FirstName} {ClientReceiver?.LastName} | " +
                   $"Courier: {Courier?.FirstName} {Courier?.LastName} | " +
                   $"Weight: {Weight}kg | Price: {Price:C} | Type: {Type} | " +
                   $"Date: {Date:yyyy-MM-dd} | Status: {Status}";
        }

    }
}
