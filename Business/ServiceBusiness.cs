using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ServiceBusiness:IDisposable
    {
        private readonly ExpressDbContext _context;
        private readonly bool _contextOwned;
        public ServiceBusiness(ExpressDbContext context)
        {
            _context = context;
            _contextOwned = false;
        }

        public ServiceBusiness()
        {
            _context = new ExpressDbContext();
            _contextOwned = true;
        }
        public async Task<List<Service>> GetAllServices()
        {
            return await _context.Services.ToListAsync();
        }
        public async Task<Service> GetServiceById(int id)
        {
            return await _context.Services.FindAsync(id);
        }
        public async Task AddService(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateService(Service service)
        {
            var item = await _context.Clients.FindAsync(service.Id);
            if (item != null)
            {
                _context.Entry(item).CurrentValues.SetValues(service);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
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
