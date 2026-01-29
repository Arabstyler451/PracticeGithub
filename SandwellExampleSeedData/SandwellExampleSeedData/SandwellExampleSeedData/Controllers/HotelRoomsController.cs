using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SandwellExampleSeedData.Data;
using SandwellExampleSeedData.Models;

namespace SandwellExampleSeedData.Controllers
{
    public class HotelRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HotelRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.HotelRoom.ToListAsync());
        }

        // GET: HotelRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return View(hotelRoom);
        }

        // GET: HotelRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HotelRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,PricePerNight,StarRating,GuestRating,NumberOfBedrooms,IsAvailable")] HotelRoom hotelRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotelRoom);
        }

        // GET: HotelRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom.FindAsync(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }
            return View(hotelRoom);
        }

        // POST: HotelRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,PricePerNight,StarRating,GuestRating,NumberOfBedrooms,IsAvailable")] HotelRoom hotelRoom)
        {
            if (id != hotelRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelRoomExists(hotelRoom.Id))
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
            return View(hotelRoom);
        }

        // GET: HotelRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelRoom = await _context.HotelRoom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            return View(hotelRoom);
        }

        // POST: HotelRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotelRoom = await _context.HotelRoom.FindAsync(id);
            if (hotelRoom != null)
            {
                _context.HotelRoom.Remove(hotelRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FilterByBedrooms(int bedrooms)
        {
            var filteredHotels = await _context.HotelRoom.Where(h => h.NumberOfBedrooms == bedrooms).ToListAsync();
            return View("Index", filteredHotels);
        }

        public async Task<IActionResult> AvailableRooms()
        {
            var availableHotels = await _context.HotelRoom.Where(h => h.IsAvailable == true).ToListAsync();
            return View("Index", availableHotels);
        }

        public async Task<IActionResult> FilterByPrice(double minPrice, double maxPrice)
        {
            var filteredHotels = await _context.HotelRoom.Where(h => h.PricePerNight >= minPrice && h.PricePerNight <= maxPrice).ToListAsync();
            return View("Index", filteredHotels);
        }

        public async Task<IActionResult> getAvailableRoomsBetweenDates(DateTime startDate, DateTime endDate)
        {
            var allRooms = await _context.HotelRoom.Include(h => h.Bookings).ToListAsync();

          
            var availableRooms = allRooms.Where(h => !h.Bookings.Any(b => (startDate < b.EndDate) && (endDate > b.StartDate))).ToList();

            return View("Index", availableRooms); 

        }

        public async Task<IActionResult> SortByGuestRating(bool ascending)
        {
            if (ascending == true)
            {
                var sortedHotels = await _context.HotelRoom.OrderBy(h => h.GuestRating).ToListAsync();
                return View("Index", sortedHotels);
            }
            else if (ascending == false)
            {
                var sortedHotels = await _context.HotelRoom.OrderByDescending(h => h.GuestRating).ToListAsync();
                return View("Index", sortedHotels);
            }
            return View("Index");
        }

        private bool HotelRoomExists(int id)
        {
            return _context.HotelRoom.Any(e => e.Id == id);
        }
    }
}
