using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int CourierId { get; set; }

        public Client Sender { get; set; }
        public Client Receiver { get; set; }
        public Courier Courier { get; set; }

        public ICollection<ShipmentService> ShipmentServices { get; set; }
    }
}
