using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Test.Data;
using Test.Models;

namespace Test.Controllers
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

            var userBookingData = await _context.Booking.Where(h => h.UserId == userId).ToListAsync();

            return View(userBookingData);
        }

        // GET: Bookings/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var BookingData = await _context.Booking.FirstOrDefaultAsync(r => r.BookingId == id && r.UserId == UserId);
            if (BookingData == null)
            {
                return Unauthorized();
            }
            return View(BookingData);
        }

        // GET: Bookings/Create
        [Authorize]
        public IActionResult Create(int roomId)
        {
            // Load rooms for dropdown
            var rooms = _context.Room.ToList();
            ViewBag.RoomsList = rooms;  // Use different name to avoid conflict

            ViewBag.RoomId = roomId;
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,RoomId,BookingDate,StartTime,EndTime,Status")] Booking booking)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserId == null)
            {
                return NotFound();
            }

            booking.UserId = UserId;
            ModelState.Remove("UserId");


            var TheRoom = await _context.Room.FindAsync(booking.RoomId);
            if (TheRoom == null)
            {
                return View(booking);
            }
            booking.Room = TheRoom;

            // DATE VALIDATION: Check if booking date is in the past
            if (booking.BookingDate < DateTime.Today)
            {
                ModelState.AddModelError("BookingDate", "Booking date cannot be in the past.");
            }

            // TIME VALIDATION: Check if end time is after start time
            if (booking.EndTime <= booking.StartTime)
            {
                ModelState.AddModelError("EndTime", "End time must be after start time.");
            }

            ModelState.Remove("Room");


            if (!ModelState.IsValid)
            {
                // Reload rooms for dropdown
                ViewBag.RoomsList = await _context.Room.ToListAsync();
                return View(booking);
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var BookingData = await _context.Booking.FirstOrDefaultAsync(r => r.BookingId == id && r.UserId == UserId);
            if (BookingData == null)
            {
                return Unauthorized();
            }
            return View(BookingData);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,RoomId,BookingDate,StartTime,EndTime,Status")] Booking booking)
        {
            var TheRoom = await _context.Room.FindAsync(booking.RoomId);
            if (TheRoom == null)
            {
                return View(booking);
            }
            booking.Room = TheRoom;

            // DATE VALIDATION: Check if booking date is in the past
            if (booking.BookingDate < DateTime.Today)
            {
                ModelState.AddModelError("BookingDate", "Booking date cannot be in the past.");
            }

            // TIME VALIDATION: Check if end time is after start time
            if (booking.EndTime <= booking.StartTime)
            {
                ModelState.AddModelError("EndTime", "End time must be after start time.");
            }

            ModelState.Remove("Room");


            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserId == null)
            {
                return NotFound();
            }

            booking.UserId = UserId;
            ModelState.Remove("UserId");

            if (id != booking.BookingId)
            {
                return NotFound();
            }

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
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize]
        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingId == id);
        }

