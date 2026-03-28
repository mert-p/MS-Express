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
    public class ShipmentsController : Controller
    {
        private readonly ExpressDbContext _context;

        public ShipmentsController(ExpressDbContext context)
        {
            _context = context;
        }

        // GET: Shipments
        public async Task<IActionResult> Index()
        {
            var expressDbContext = _context.Shipments.Include(s => s.ClientReceiver).Include(s => s.ClientSender).Include(s => s.Courier);
            return View(await expressDbContext.ToListAsync());
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.ClientReceiver)
                .Include(s => s.ClientSender)
                .Include(s => s.Courier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // GET: Shipments/Create
        public IActionResult Create()
        {
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address");
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address");
            ViewData["CourierId"] = new SelectList(_context.Couriers, "Id", "FirstName");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SenderId,ReceiverId,CourierId,Weight,Price,Type,Date,Status")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address", shipment.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address", shipment.SenderId);
            ViewData["CourierId"] = new SelectList(_context.Couriers, "Id", "FirstName", shipment.CourierId);
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address", shipment.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address", shipment.SenderId);
            ViewData["CourierId"] = new SelectList(_context.Couriers, "Id", "FirstName", shipment.CourierId);
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SenderId,ReceiverId,CourierId,Weight,Price,Type,Date,Status")] Shipment shipment)
        {
            if (id != shipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(shipment.Id))
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
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address", shipment.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address", shipment.SenderId);
            ViewData["CourierId"] = new SelectList(_context.Couriers, "Id", "FirstName", shipment.CourierId);
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.ClientReceiver)
                .Include(s => s.ClientSender)
                .Include(s => s.Courier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment != null)
            {
                _context.Shipments.Remove(shipment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.Id == id);
        }
    }
}
