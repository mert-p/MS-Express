using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels
{
    public abstract class PersonViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} | Name: {FullName} | Phone: {Phone}";
        }
    }
}
