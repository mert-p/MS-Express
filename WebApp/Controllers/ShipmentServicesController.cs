using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;

namespace WebApp.Controllers
{
    public class ShipmentServicesController : Controller
    {
        private readonly ExpressDbContext _context;

        public ShipmentServicesController(ExpressDbContext context)
        {
            _context = context;
        }

        // GET: ShipmentServices
        public async Task<IActionResult> Index()
        {
            var expressDbContext = _context.ShipmentServices
                .Include(s => s.Service)
                .Include(s => s.Shipment);
            return View(await expressDbContext.ToListAsync());
        }

        // GET: ShipmentServices/Details/5
        public async Task<IActionResult> Details(int? shipmentId, int? serviceId)
        {
            if (shipmentId == null || serviceId == null)
            {
                return NotFound();
            }

            var shipmentService = await _context.ShipmentServices
                .Include(s => s.Service)
                .Include(s => s.Shipment)
                .FirstOrDefaultAsync(m => m.ShipmentId == shipmentId && m.ServiceId == serviceId);

            if (shipmentService == null)
            {
                return NotFound();
            }

            return View(shipmentService);
        }

        // GET: ShipmentServices/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Status");
            return View();
        }

        // POST: ShipmentServices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipmentId,ServiceId,ExtraPrice,Notes")] ShipmentService shipmentService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipmentService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", shipmentService.ServiceId);
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Status", shipmentService.ShipmentId);
            return View(shipmentService);
        }

        // GET: ShipmentServices/Edit
        public async Task<IActionResult> Edit(int? shipmentId, int? serviceId)
        {
            if (shipmentId == null || serviceId == null)
            {
                return NotFound();
            }

            var shipmentService = await _context.ShipmentServices
                .FindAsync(shipmentId, serviceId);

            if (shipmentService == null)
            {
                return NotFound();
            }

            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", shipmentService.ServiceId);
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Status", shipmentService.ShipmentId);
            return View(shipmentService);
        }

        // POST: ShipmentServices/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int shipmentId, int serviceId, [Bind("ShipmentId,ServiceId,ExtraPrice,Notes")] ShipmentService shipmentService)
        {
            if (shipmentId != shipmentService.ShipmentId || serviceId != shipmentService.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipmentService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentServiceExists(shipmentId, serviceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", shipmentService.ServiceId);
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Status", shipmentService.ShipmentId);
            return View(shipmentService);
        }

        // GET: ShipmentServices/Delete
        public async Task<IActionResult> Delete(int? shipmentId, int? serviceId)
        {
            if (shipmentId == null || serviceId == null)
            {
                return NotFound();
            }

            var shipmentService = await _context.ShipmentServices
                .Include(s => s.Service)
                .Include(s => s.Shipment)
                .FirstOrDefaultAsync(m => m.ShipmentId == shipmentId && m.ServiceId == serviceId);

            if (shipmentService == null)
            {
                return NotFound();
            }

            return View(shipmentService);
        }

        // POST: ShipmentServices/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int shipmentId, int serviceId)
        {
            var shipmentService = await _context.ShipmentServices
                .FindAsync(shipmentId, serviceId);

            if (shipmentService != null)
            {
                _context.ShipmentServices.Remove(shipmentService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentServiceExists(int shipmentId, int serviceId)
        {
            return _context.ShipmentServices
                .Any(e => e.ShipmentId == shipmentId && e.ServiceId == serviceId);
        }
    }
}
