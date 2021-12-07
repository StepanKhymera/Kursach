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
    public class ClaimPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClaimPayments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClaimPayments.Include(c => c.Claim);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClaimPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimPayment = await _context.ClaimPayments
                .Include(c => c.Claim)
                .FirstOrDefaultAsync(m => m.ClaimPaymentId == id);
            if (claimPayment == null)
            {
                return NotFound();
            }

            return View(claimPayment);
        }

        // GET: ClaimPayments/Create
        public IActionResult Create()
        {
            ViewData["ClaimId"] = new SelectList(_context.Claims, "ClaimId", "ClaimId");
            return View();
        }

        // POST: ClaimPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimPaymentId,Date,Sum,ClaimId")] ClaimPayment claimPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claimPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClaimId"] = new SelectList(_context.Claims, "ClaimId", "ClaimId", claimPayment.ClaimId);
            return View(claimPayment);
        }

        // GET: ClaimPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimPayment = await _context.ClaimPayments.FindAsync(id);
            if (claimPayment == null)
            {
                return NotFound();
            }
            ViewData["ClaimId"] = new SelectList(_context.Claims, "ClaimId", "ClaimId", claimPayment.ClaimId);
            return View(claimPayment);
        }

        // POST: ClaimPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClaimPaymentId,Date,Sum,ClaimId")] ClaimPayment claimPayment)
        {
            if (id != claimPayment.ClaimPaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claimPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimPaymentExists(claimPayment.ClaimPaymentId))
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
            ViewData["ClaimId"] = new SelectList(_context.Claims, "ClaimId", "ClaimId", claimPayment.ClaimId);
            return View(claimPayment);
        }

        // GET: ClaimPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claimPayment = await _context.ClaimPayments
                .Include(c => c.Claim)
                .FirstOrDefaultAsync(m => m.ClaimPaymentId == id);
            if (claimPayment == null)
            {
                return NotFound();
            }

            return View(claimPayment);
        }

        // POST: ClaimPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claimPayment = await _context.ClaimPayments.FindAsync(id);
            _context.ClaimPayments.Remove(claimPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimPaymentExists(int id)
        {
            return _context.ClaimPayments.Any(e => e.ClaimPaymentId == id);
        }
    }
}
