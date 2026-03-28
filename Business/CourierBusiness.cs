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


        public async Task<List<CourierViewModel>> GetAllCouriersViews()
        {
            var couriers = await GetAll();
            return couriers.Select(MapToViewModel).ToList();
        }        
        public async Task<List<CourierViewModel>> GetAvailableCouriers()
        {
            var couriers = await _context.Couriers.Where(c => c.Available == true).ToListAsync();
            return couriers.Select(MapToViewModel).ToList();
        }
        public async Task<List<CourierViewModel>> GetCouriersByMostSalary()
        {
            var couriers = await _context.Couriers.OrderByDescending(c=>c.Salary).ToListAsync();
            return couriers.Select(MapToViewModel).ToList();
        }
        public async Task<List<CourierViewModel>> GetCouriersByLeastSalary()
        {
            var couriers = await _context.Couriers.OrderBy(c => c.Salary).ToListAsync();
            return couriers.Select(MapToViewModel).ToList();
        }
        public async Task<CourierViewModel> GetCourierViewById(int id)
        {
            var courier = await GetById(id);
            return MapToViewModel(courier);
        }
        public async Task<Courier> GetCourierByIdWithShipments(int id)
        {
            return await _context.Couriers.Include(c => c.Shipments).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Courier> GetCourierByIdWithShipmentsAndClients(int id)
        {
            return await _context.Couriers.Include(c => c.Shipments).ThenInclude(c=>c.ClientReceiver).Include(c => c.Shipments).ThenInclude(c => c.ClientSender).FirstOrDefaultAsync(c => c.Id == id);
        }

        private CourierViewModel MapToViewModel(Courier c)
        {
            return new CourierViewModel
            {
                Id = c.Id,
                FullName = $"{c.FirstName} {c.LastName}",
                Phone = c.Phone,
                DisplaySalary = $"{c.Salary:F2}",
                Availability = c.Available ? "Available" : "Not Available"
            };
        }
    }
}
