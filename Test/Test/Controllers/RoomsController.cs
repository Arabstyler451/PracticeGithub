using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Room.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,Name,Description,Capacity,Equipment,PricePerHour,StarRating,GuestRating,NumberOfRooms,IsAvailable")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,Name,Description,Capacity,Equipment,PricePerHour,StarRating,GuestRating,NumberOfRooms,IsAvailable")] Room room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomId))
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
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room != null)
            {
                _context.Room.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.RoomId == id);
        }
        public async Task<IActionResult> FilterByRooms(int rooms)
        {
            var FilteredRooms = await _context.Room.Where(r => r.NumberOfRooms == rooms).ToListAsync();
            return View("Index", FilteredRooms);
        }
        public async Task<IActionResult> AvailableRooms(int rooms)
        {
            var AvailableRooms = await _context.Room.Where(r => r.IsAvailable == true).ToListAsync();
            return View("Index", AvailableRooms);
        }

        public async Task<IActionResult> SortByGuestRating(bool ascending)
        {
            if (ascending == true)
            {
                var SortedRooms = await _context.Room.OrderBy(a => a.GuestRating).ToListAsync();
                return View("Index", SortedRooms);
            }
            else if (ascending == false)
            {
                var SortedRooms = await _context.Room.OrderByDescending(d => d.GuestRating).ToListAsync();
                return View("Index", SortedRooms);
            }
            return View("Index");
        }
        public async Task<IActionResult> FilterByPrice(float? minPrice, float? maxPrice)
        {
            IQueryable<Room> query = _context.Room;

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                // Both values provided - range filter
                query = query.Where(r => r.PricePerHour >= minPrice.Value &&
                                        r.PricePerHour <= maxPrice.Value);
            }
            else if (minPrice.HasValue && !maxPrice.HasValue)
            {
                // Only min provided - get rooms with price >= minPrice
                query = query.Where(r => r.PricePerHour >= minPrice.Value);
            }
            else if (!minPrice.HasValue && maxPrice.HasValue)
            {
                // Only max provided - get rooms with price <= maxPrice
                query = query.Where(r => r.PricePerHour <= maxPrice.Value);
            }
            // else: neither provided - return all rooms

            var filteredRooms = await query.ToListAsync();
            return View("Index", filteredRooms);
        }
        public async Task<IActionResult> SortByStarRating(bool ascending)
        {
            if (ascending == true)
            {
                var SortedRooms = await _context.Room.OrderBy(a => a.StarRating).ToListAsync();
                return View("Index", SortedRooms);
            }
            else if (ascending == false)
            {
                var SortedRooms = await _context.Room.OrderByDescending(d => d.StarRating).ToListAsync();
                return View("Index", SortedRooms);
            }
            return View("Index");
        }

        public async Task<IActionResult> FilterByCapacity(int? minCapacity, int? maxCapacity)
        {
            IQueryable<Room> query = _context.Room;

            if (minCapacity.HasValue && maxCapacity.HasValue)
            {
                // Both values provided - range filter
                query = query.Where(r => r.Capacity >= minCapacity.Value &&
                                        r.Capacity <= maxCapacity.Value);
            }
            else if (minCapacity.HasValue && !maxCapacity.HasValue)
            {
                // Only min provided - get rooms with price >= minCapacity
                query = query.Where(r => r.Capacity >= minCapacity.Value);
            }
            else if (!minCapacity.HasValue && maxCapacity.HasValue)
            {
                // Only max provided - get rooms with price <= maxCapacity
                query = query.Where(r => r.Capacity <= maxCapacity.Value);
            }
            // else: neither provided - return all rooms

            var filteredRooms = await query.ToListAsync();
            return View("Index", filteredRooms);
        }


    }
}
