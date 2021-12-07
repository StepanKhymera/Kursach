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
    public class Types_Of_InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Types_Of_InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Types_Of_Insurances
        public async Task<IActionResult> Index()
        {
            return View(await _context.Types_Of_Insurances.ToListAsync());
        }

        // GET: Types_Of_Insurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Types_Of_Insurance = await _context.Types_Of_Insurances
                .FirstOrDefaultAsync(m => m.InsuranceTypeCode == id);
            if (Types_Of_Insurance == null)
            {
                return NotFound();
            }

            return View(Types_Of_Insurance);
        }

        // GET: Types_Of_Insurances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Types_Of_Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceTypeCode,TypeDescription,AveragePricePerYear,AverageRisk,AverageCoverage")] Types_Of_Insurance Types_Of_Insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Types_Of_Insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Types_Of_Insurance);
        }

        // GET: Types_Of_Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Types_Of_Insurance = await _context.Types_Of_Insurances.FindAsync(id);
            if (Types_Of_Insurance == null)
            {
                return NotFound();
            }
            return View(Types_Of_Insurance);
        }

        // POST: Types_Of_Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceTypeCode,TypeDescription,AveragePricePerYear,AverageRisk,AverageCoverage")] Types_Of_Insurance Types_Of_Insurance)
        {
            if (id != Types_Of_Insurance.InsuranceTypeCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Types_Of_Insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Types_Of_InsuranceExists(Types_Of_Insurance.InsuranceTypeCode))
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
            return View(Types_Of_Insurance);
        }

        // GET: Types_Of_Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Types_Of_Insurance = await _context.Types_Of_Insurances
                .FirstOrDefaultAsync(m => m.InsuranceTypeCode == id);
            if (Types_Of_Insurance == null)
            {
                return NotFound();
            }

            return View(Types_Of_Insurance);
        }

        // POST: Types_Of_Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Types_Of_Insurance = await _context.Types_Of_Insurances.FindAsync(id);
            _context.Types_Of_Insurances.Remove(Types_Of_Insurance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Types_Of_InsuranceExists(int id)
        {
            return _context.Types_Of_Insurances.Any(e => e.InsuranceTypeCode == id);
        }
    }
}
