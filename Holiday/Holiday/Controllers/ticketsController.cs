using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Holiday.Data;
using Holiday.Models;

namespace Holiday.Controllers
{
    public class ticketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ticketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ticket.Include(t => t.Flight);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.ticket
                .Include(t => t.Flight)
                .FirstOrDefaultAsync(m => m.ticketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: tickets/Create
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Flight, "FlightId", "FlightId");
            return View();
        }

        // POST: tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ticketId,ticketName,ticketType,ticketDate,ticketTime,FlightId")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flight, "FlightId", "FlightId", ticket.FlightId);
            return View(ticket);
        }

        // GET: tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flight, "FlightId", "FlightId", ticket.FlightId);
            return View(ticket);
        }

        // POST: tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ticketId,ticketName,ticketType,ticketDate,ticketTime,FlightId")] ticket ticket)
        {
            if (id != ticket.ticketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ticketExists(ticket.ticketId))
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
            ViewData["FlightId"] = new SelectList(_context.Flight, "FlightId", "FlightId", ticket.FlightId);
            return View(ticket);
        }

        // GET: tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.ticket
                .Include(t => t.Flight)
                .FirstOrDefaultAsync(m => m.ticketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ticketExists(int id)
        {
            return _context.ticket.Any(e => e.ticketId == id);
        }
    }
}
