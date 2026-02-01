using CityHome.Data;
using CityHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CityHome.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userBookingData = await _context.Booking.Where(g => g.UserId == userId).ToListAsync();
            return View(userBookingData);

            //r applicationDbContext = _context.Booking.Include(b => b.Room);
           //eturn View(await applicationDbContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Booking = await _context.Booking.FirstOrDefaultAsync(g => g.BookingId == id && g.UserId == userId);
            if (Booking == null)
                return Unauthorized();
            return View(Booking);
        }

        // GET: Bookings/Create
        [Authorize]

        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,BookingDate,TimeSlot,Status")] Booking booking)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }
            booking.UserId = userId;
            ModelState.Remove("UserID");

            var room = await _context.Room.FindAsync(booking.RoomId);

            if (room == null)
            {
                return NotFound();
            }
            booking.Room = room;
            ModelState.Remove("Room");

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Booking = await _context.Booking.FirstOrDefaultAsync(g => g.BookingId == id && g.UserId == userId);
            if (Booking == null)
                return Unauthorized();
            return View(Booking);

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var booking = await _context.Booking.FindAsync(id);
            //if (booking == null)
            //{
            //    return NotFound();
           // }
           // ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", booking.RoomId);
            //return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingDate,TimeSlot,Status")] Booking booking)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }
            booking.UserId = userId;
            ModelState.Remove("UserID");

            if (id != booking.BookingId)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(booking.RoomId);

            if (room == null)
            {
                return NotFound();
            }
            booking.Room = room;
            ModelState.Remove("Room");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
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
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingId == id);
        }
    }
}
