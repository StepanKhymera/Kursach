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
    public class PolicyPaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicyPaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PolicyPayments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PolicyPayments.Include(p => p.Policy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PolicyPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyPayment = await _context.PolicyPayments
                .Include(p => p.Policy)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (policyPayment == null)
            {
                return NotFound();
            }

            return View(policyPayment);
        }

        // GET: PolicyPayments/Create
        public IActionResult Create()
        {
            ViewData["PolicyId"] = new SelectList(_context.Policies, "PolicyId", "PolicyId");
            return View();
        }

        // POST: PolicyPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,PolicyId,Date,Sum")] PolicyPayment policyPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PolicyId"] = new SelectList(_context.Policies, "PolicyId", "PolicyId", policyPayment.PolicyId);
            return View(policyPayment);
        }

        // GET: PolicyPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyPayment = await _context.PolicyPayments.FindAsync(id);
            if (policyPayment == null)
            {
                return NotFound();
            }
            ViewData["PolicyId"] = new SelectList(_context.Policies, "PolicyId", "PolicyId", policyPayment.PolicyId);
            return View(policyPayment);
        }

        // POST: PolicyPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,PolicyId,Date,Sum")] PolicyPayment policyPayment)
        {
            if (id != policyPayment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyPaymentExists(policyPayment.PaymentId))
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
            ViewData["PolicyId"] = new SelectList(_context.Policies, "PolicyId", "PolicyId", policyPayment.PolicyId);
            return View(policyPayment);
        }

        // GET: PolicyPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyPayment = await _context.PolicyPayments
                .Include(p => p.Policy)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (policyPayment == null)
            {
                return NotFound();
            }

            return View(policyPayment);
        }

        // POST: PolicyPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyPayment = await _context.PolicyPayments.FindAsync(id);
            _context.PolicyPayments.Remove(policyPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyPaymentExists(int id)
        {
            return _context.PolicyPayments.Any(e => e.PaymentId == id);
        }
    }
}
