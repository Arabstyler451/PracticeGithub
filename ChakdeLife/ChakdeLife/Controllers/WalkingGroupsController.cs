using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChakdeLife.Data;
using ChakdeLife.Models;

namespace ChakdeLife.Controllers
{
    public class WalkingGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalkingGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WalkingGroups
        public async Task<IActionResult> Index()
        {
            return View(await _context.WalkingGroup.ToListAsync());
        }

        // GET: WalkingGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroup = await _context.WalkingGroup
                .FirstOrDefaultAsync(m => m.WalkingGroupId == id);
            if (walkingGroup == null)
            {
                return NotFound();
            }

            return View(walkingGroup);
        }

        // GET: WalkingGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WalkingGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WalkingGroupId,Name,Description,Location,EventDate,StartTime,EndTime")] WalkingGroup walkingGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(walkingGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(walkingGroup);
        }

        // GET: WalkingGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroup = await _context.WalkingGroup.FindAsync(id);
            if (walkingGroup == null)
            {
                return NotFound();
            }
            return View(walkingGroup);
        }

        // POST: WalkingGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WalkingGroupId,Name,Description,Location,EventDate,StartTime,EndTime")] WalkingGroup walkingGroup)
        {
            if (id != walkingGroup.WalkingGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(walkingGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalkingGroupExists(walkingGroup.WalkingGroupId))
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
            return View(walkingGroup);
        }

        // GET: WalkingGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroup = await _context.WalkingGroup
                .FirstOrDefaultAsync(m => m.WalkingGroupId == id);
            if (walkingGroup == null)
            {
                return NotFound();
            }

            return View(walkingGroup);
        }

        // POST: WalkingGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walkingGroup = await _context.WalkingGroup.FindAsync(id);
            if (walkingGroup != null)
            {
                _context.WalkingGroup.Remove(walkingGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalkingGroupExists(int id)
        {
            return _context.WalkingGroup.Any(e => e.WalkingGroupId == id);
        }
    }
}
