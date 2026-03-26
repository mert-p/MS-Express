using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels
{
    public class CourierViewModel : PersonViewModel
    {
        public string DisplaySalary { get; set; }    
        public string Availability { get; set; }      

        public override string ToString()
        {
            return $"{base.ToString()} | Salary: {DisplaySalary} | Status: {Availability}";
        }
    }
}
