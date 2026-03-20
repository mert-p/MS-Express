using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string DeliveryCompany { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}
