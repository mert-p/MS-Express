using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ServiceBusiness: BaseBusiness<Service>
    {
        public ServiceBusiness(ExpressDbContext context) : base(context) { }
        public ServiceBusiness() : base() { }
        private ServiceViewModel MapToViewModel(Service s)
        {
            return new ServiceViewModel
            {
                Id = s.Id,
                Name = s.Name,
                DisplayPrice = $"{s.Price:C}"
            };
        }
    }
}
