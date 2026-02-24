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
    public class WalkingGroupMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalkingGroupMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WalkingGroupMembers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WalkingGroupMember.Include(w => w.WalkingGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WalkingGroupMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroupMember = await _context.WalkingGroupMember
                .Include(w => w.WalkingGroup)
                .FirstOrDefaultAsync(m => m.WalkingGroupMemberId == id);
            if (walkingGroupMember == null)
            {
                return NotFound();
            }

            return View(walkingGroupMember);
        }

        // GET: WalkingGroupMembers/Create
        public IActionResult Create()
        {
            ViewData["WalkingGroupId"] = new SelectList(_context.WalkingGroup, "WalkingGroupId", "WalkingGroupId");
            return View();
        }

        // POST: WalkingGroupMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WalkingGroupMemberId,WalkingGroupId,UserId,JoinedDate")] WalkingGroupMember walkingGroupMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(walkingGroupMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WalkingGroupId"] = new SelectList(_context.WalkingGroup, "WalkingGroupId", "WalkingGroupId", walkingGroupMember.WalkingGroupId);
            return View(walkingGroupMember);
        }

        // GET: WalkingGroupMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroupMember = await _context.WalkingGroupMember.FindAsync(id);
            if (walkingGroupMember == null)
            {
                return NotFound();
            }
            ViewData["WalkingGroupId"] = new SelectList(_context.WalkingGroup, "WalkingGroupId", "WalkingGroupId", walkingGroupMember.WalkingGroupId);
            return View(walkingGroupMember);
        }

        // POST: WalkingGroupMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WalkingGroupMemberId,WalkingGroupId,UserId,JoinedDate")] WalkingGroupMember walkingGroupMember)
        {
            if (id != walkingGroupMember.WalkingGroupMemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(walkingGroupMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalkingGroupMemberExists(walkingGroupMember.WalkingGroupMemberId))
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
            ViewData["WalkingGroupId"] = new SelectList(_context.WalkingGroup, "WalkingGroupId", "WalkingGroupId", walkingGroupMember.WalkingGroupId);
            return View(walkingGroupMember);
        }

        // GET: WalkingGroupMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walkingGroupMember = await _context.WalkingGroupMember
                .Include(w => w.WalkingGroup)
                .FirstOrDefaultAsync(m => m.WalkingGroupMemberId == id);
            if (walkingGroupMember == null)
            {
                return NotFound();
            }

            return View(walkingGroupMember);
        }

        // POST: WalkingGroupMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walkingGroupMember = await _context.WalkingGroupMember.FindAsync(id);
            if (walkingGroupMember != null)
            {
                _context.WalkingGroupMember.Remove(walkingGroupMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalkingGroupMemberExists(int id)
        {
            return _context.WalkingGroupMember.Any(e => e.WalkingGroupMemberId == id);
        }
    }
}
