using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}
