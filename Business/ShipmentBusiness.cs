using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ShipmentBusiness : IDisposable
    {
        private readonly ExpressDbContext _context;
        private readonly bool _contextOwned;
        public ShipmentBusiness(ExpressDbContext context)
        {
            _context = context;
            _contextOwned = false;
        }

        public ShipmentBusiness()
        {
            _context = new ExpressDbContext();
            _contextOwned = true;
        }
        public async Task<List<Shipment>> GetAllClients()
        {
            return await _context.Shipments.ToListAsync();
        }
        public async Task<Shipment> GetClientById(int id)
        {
            return await _context.Shipments.FindAsync(id);
        }
        public async Task AddClient(Shipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateClient(Shipment shipment)
        {
            var item = await _context.Shipments.FindAsync(shipment.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(shipment);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteClient(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
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
