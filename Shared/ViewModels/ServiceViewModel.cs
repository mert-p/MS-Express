using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayPrice { get; set; }   

        public override string ToString()
        {
            return $"ID: {Id} | Name: {Name} | Price: {DisplayPrice}";
        }
    }
}
