using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ShipmentBusiness : BaseBusiness<Shipment>
    {
        public ShipmentBusiness(ExpressDbContext context) : base(context) { }
        public ShipmentBusiness() : base() { }
        public async Task<int> AddWithId(Shipment shipment)
        {
            await _dbSet.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment.Id;
        }
        public async Task<List<Shipment>> GetAllShipments()
        {
            return await _context.Shipments.Include(c => c.ClientReceiver).Include(c=>c.ClientSender).Include(c=>c.Courier).ToListAsync();
        }
        public async Task<List<Shipment>> GetShipmentsWirhService()
        {
            return await _context.Shipments.Include(c => c.ClientReceiver).Include(c => c.ClientSender).Include(c => c.Courier).Include(c => c.ShipmentServices).ThenInclude(c => c.Service).ToListAsync();
        }
        public async Task<Shipment> GetShipmentWirhService(int id)
        {
            return await _context.Shipments.Include(c => c.ClientReceiver).Include(c => c.ClientSender).Include(c => c.Courier).Include(c => c.ShipmentServices).ThenInclude(c => c.Service).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<Shipment>> GetShipmentsByStatus(string status)
        {
            return await _context.Shipments.Where(s => s.Status == status).ToListAsync();
        }
    }
}
