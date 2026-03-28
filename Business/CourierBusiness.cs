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
        public async Task<CourierViewModel> GetCourierViewById(int id)
        {
            var courier = await GetById(id);
            return MapToViewModel(courier);
        }

        public async Task<List<CourierViewModel>> GetAllCouriersViews()
        {
            var couriers = await GetAll();
            return couriers.Select(MapToViewModel).ToList();
        }
        public async Task<CourierViewModel> GetCourierByIdWithShipments(int id)
        {
            Courier courier = await _context.Couriers.Include(c => c.Shipments).FirstOrDefaultAsync(c => c.Id == id);
            return MapToViewModel(courier);
        }
        public async Task<CourierViewModel> GetCourierByIdWithShipmentsAndClients(int id)
        {
            Courier courier = await _context.Couriers.Include(c => c.Shipments).ThenInclude(c=>c.ReceiverId).Include(c => c.Shipments).ThenInclude(c => c.SenderId).FirstOrDefaultAsync(c => c.Id == id);
            return MapToViewModel(courier);
        }

        private CourierViewModel MapToViewModel(Courier c)
        {
            return new CourierViewModel
            {
                Id = c.Id,
                FullName = $"{c.FirstName} {c.LastName}",
                Phone = c.Phone,
                DisplaySalary = $"{c.Salary:C}",
                Availability = c.Available ? "Available" : "Not Available"
            };
        }
    }
}
