using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ShipmentServiceBusiness : IDisposable
    {
        private readonly ExpressDbContext _context;
        private readonly bool _contextOwned;
        public ShipmentServiceBusiness(ExpressDbContext context)
        {
            _context = context;
            _contextOwned = false;
        }

        public ShipmentServiceBusiness()
        {
            _context = new ExpressDbContext();
            _contextOwned = true;
        }
        public async Task<List<ShipmentService>> GetAllShipmentServices()
        {
            return await _context.ShipmentServices.ToListAsync();
        }
        public async Task<ShipmentService> GetShipmentServiceByIds(int ShipmentId,int ServiceId)
        {
            return await _context.ShipmentServices.FindAsync(ShipmentId, ServiceId);
        }
        public async Task AddShipmentService(ShipmentService shipmentService)
        {
            await _context.ShipmentServices.AddAsync(shipmentService);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateShipmentService(ShipmentService shipmentService)
        {
            var item = await _context.ShipmentServices.FindAsync(shipmentService.ShipmentId, shipmentService.ServiceId);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(shipmentService);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteShipmentService(int shipmentId, int serviceId)
        {
            var shipmentService = await _context.ShipmentServices.FindAsync(shipmentId, serviceId);
            if (shipmentService != null)
            {
                _context.ShipmentServices.Remove(shipmentService);
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            if (_contextOwned)
            {
                _context.Dispose();
            }
        }
    }
}
