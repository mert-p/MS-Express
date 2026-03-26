using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ServiceBusiness: BaseBusiness<Service>
    {
        public ServiceBusiness(ExpressDbContext context) : base(context) { }
        public ServiceBusiness() : base() { }
    }
}
