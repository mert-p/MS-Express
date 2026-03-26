using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels
{
    public class ClientViewModel : PersonViewModel
    {
        public string Email { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} | Email: {Email} | Address: {Address}";
        }
    }
}
