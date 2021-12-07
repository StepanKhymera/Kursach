using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kursach.Models;

namespace kursach.Controllers
{
    public class StatusesOfClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusesOfClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusesOfClaims
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusesOfClaims.ToListAsync());
        }

        // GET: StatusesOfClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusesOfClaim = await _context.StatusesOfClaims
                .FirstOrDefaultAsync(m => m.StatusCode == id);
            if (statusesOfClaim == null)
            {
                return NotFound();
            }

            return View(statusesOfClaim);
        }

        // GET: StatusesOfClaims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusesOfClaims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusCode,StatusDescription")] StatusesOfClaim statusesOfClaim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusesOfClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusesOfClaim);
        }

        // GET: StatusesOfClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusesOfClaim = await _context.StatusesOfClaims.FindAsync(id);
            if (statusesOfClaim == null)
            {
                return NotFound();
            }
            return View(statusesOfClaim);
        }

        // POST: StatusesOfClaims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusCode,StatusDescription")] StatusesOfClaim statusesOfClaim)
        {
            if (id != statusesOfClaim.StatusCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusesOfClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusesOfClaimExists(statusesOfClaim.StatusCode))
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
            return View(statusesOfClaim);
        }

        // GET: StatusesOfClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusesOfClaim = await _context.StatusesOfClaims
                .FirstOrDefaultAsync(m => m.StatusCode == id);
            if (statusesOfClaim == null)
            {
                return NotFound();
            }

            return View(statusesOfClaim);
        }

        // POST: StatusesOfClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusesOfClaim = await _context.StatusesOfClaims.FindAsync(id);
            _context.StatusesOfClaims.Remove(statusesOfClaim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusesOfClaimExists(int id)
        {
            return _context.StatusesOfClaims.Any(e => e.StatusCode == id);
        }
    }
}
