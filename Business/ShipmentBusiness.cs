using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ShipmentBusiness : BaseBusiness<Shipment>
    {
        public ShipmentBusiness(ExpressDbContext context) : base(context) { }
        public ShipmentBusiness() : base() { }

        /*public async Task<List<Shipment>> GetShipmentsByStatus(string status)
        {
            return await _context.Shipments
                .Where(s => s.Status == status)
                .ToListAsync();
        }*/
    }
}
