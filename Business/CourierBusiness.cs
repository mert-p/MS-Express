using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class CourierBusiness:IDisposable
    {
        private readonly ExpressDbContext _context;
        private readonly bool _contextOwned;
        public CourierBusiness(ExpressDbContext context)
        {
            _context = context;
            _contextOwned = false;
        }

        public CourierBusiness()
        {
            _context = new ExpressDbContext();
            _contextOwned = true;
        }
        public async Task<List<Courier>> GetAllCouriers()
        {
            return await _context.Couriers.ToListAsync();
        }
        public async Task<Courier> GetCourierById(int id)
        {
            return await _context.Couriers.FindAsync(id);
        }
        public async Task AddCourier(Courier courier)
        {
            await _context.Couriers.AddAsync(courier);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCourier(Courier courier)
        {
            var item = await _context.Couriers.FindAsync(courier.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(courier);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteCourier(int id)
        {
            var courier = await _context.Couriers.FindAsync(id);
            if (courier != null)
            {
                _context.Couriers.Remove(courier);
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
