using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToKaFitness.Data;
using ToKaFitness.Models;

namespace ToKaFitness.Controllers
{
    public class GymCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GymCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: GymCustomers
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userGymCustomerData = await _context.GymCustomer.Where(g => g.UserID == userId).ToListAsync();
            return View(userGymCustomerData);
        }

        [Authorize]
        // GET: GymCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var GymCustomers = await _context.GymCustomer.FirstOrDefaultAsync(g => g.GymCustomerId == id && g.UserID == userId);
            if (GymCustomers == null)
                return Unauthorized();
            return View(GymCustomers);
        }

        [Authorize]
        // GET: GymCustomers/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View();
        }

        // POST: GymCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymCustomerId,Name,Age,Email,Phone,Address")] GymCustomer gymCustomer)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }
            gymCustomer.UserID = userId;
            ModelState.Remove("UserID");

            if (ModelState.IsValid)
            {
                _context.Add(gymCustomer);  
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymCustomer);
        }

        [Authorize]
        // GET: GymCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var GymCustomers = await _context.GymCustomer.FirstOrDefaultAsync(g => g.GymCustomerId == id && g.UserID == userId);
            if (GymCustomers == null)
                return Unauthorized();
            return View(GymCustomers);
        }


        // POST: GymCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymCustomerId,Name,Age,Email,Phone,Address")] GymCustomer gymCustomer)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound();
            }
            gymCustomer.UserID = userId;
            ModelState.Remove("UserID");

            if (id != gymCustomer.GymCustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymCustomerExists(gymCustomer.GymCustomerId))
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
            return View(gymCustomer);
        }

        [Authorize]
        // GET: GymCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymCustomer = await _context.GymCustomer
                .FirstOrDefaultAsync(m => m.GymCustomerId == id);
            if (gymCustomer == null)
            {
                return NotFound();
            }

            return View(gymCustomer);
        }

        // POST: GymCustomers/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var gymCustomer = await _context.GymCustomer.FindAsync(id);
            if (gymCustomer != null)
            {
                _context.GymCustomer.Remove(gymCustomer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        private bool GymCustomerExists(int id)
        {
            return _context.GymCustomer.Any(e => e.GymCustomerId == id);
        }
    }
}
