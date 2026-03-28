using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class CourierBusiness: BaseBusiness<Courier>
    {
        public CourierBusiness(ExpressDbContext context) : base(context) { }
        public CourierBusiness() : base() { }


      
        public async Task<List<Courier>> GetAvailableCouriers()
        {
            return await _context.Couriers.Where(c => c.Available == true).ToListAsync();
        }
        public async Task<List<Courier>> GetCouriersByMostSalary()
        {
            return await _context.Couriers.OrderByDescending(c=>c.Salary).ToListAsync();
        }
        public async Task<List<Courier>> GetCouriersByLeastSalary()
        {
            return await _context.Couriers.OrderBy(c => c.Salary).ToListAsync();
        }

        public async Task<Courier> GetCourierByIdWithShipments(int id)
        {
            return await _context.Couriers.Include(c => c.Shipments).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Courier> GetCourierByIdWithShipmentsAndClients(int id)
        {
            return await _context.Couriers.Include(c => c.Shipments).ThenInclude(c=>c.ClientReceiver).Include(c => c.Shipments).ThenInclude(c => c.ClientSender).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
