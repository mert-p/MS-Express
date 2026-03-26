using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class CourierBusiness: BaseBusiness<Courier>
    {
        public CourierBusiness(ExpressDbContext context) : base(context) { }
        public CourierBusiness() : base() { }
    }
}
