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
        public async Task<ShipmentViewModel> GetShipmentViewById(int id)
        {
            var shipment = await GetById(id);
            return MapToViewModel(shipment);
        }

        public async Task<List<ShipmentViewModel>> GetAllShipmentViews()
        {
            var shipments = await GetAll();
            return shipments.Select(MapToViewModel).ToList();
        }
        private ShipmentViewModel MapToViewModel(Shipment s)
        {
            return new ShipmentViewModel
            {
                Id = s.Id,
                SenderFullName = $"{s.ClientSender?.FirstName} {s.ClientSender?.LastName}",
                ReceiverFullName = $"{s.ClientReceiver?.FirstName} {s.ClientReceiver?.LastName}",
                CourierFullName = $"{s.Courier?.FirstName} {s.Courier?.LastName}",
                Weight = $"{s.Weight}kg",
                DisplayPrice = $"{s.Price:F2}",
                Type = s.Type,
                Date = s.Date.ToString("yyyy-MM-dd"),
                Status = s.Status
            };
        }
    }
}
