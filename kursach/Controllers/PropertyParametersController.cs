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
    public class PropertyParametersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyParametersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropertyParameters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropertyParameters.Include(p => p.TypeOfInsuranceNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertyParameters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyParameter = await _context.PropertyParameters
                .Include(p => p.TypeOfInsuranceNavigation)
                .FirstOrDefaultAsync(m => m.ParamaterId == id);
            if (propertyParameter == null)
            {
                return NotFound();
            }

            return View(propertyParameter);
        }

        // GET: PropertyParameters/Create
        public IActionResult Create()
        {
            ViewData["TypeOfInsurance"] = new SelectList(_context.Types_Of_Insurances, "InsuranceTypeCode", "TypeDescription");
            return View();
        }

        // POST: PropertyParameters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParamaterId,ParameterDescription,TypeOfInsurance")] PropertyParameter propertyParameter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyParameter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeOfInsurance"] = new SelectList(_context.Types_Of_Insurances, "InsuranceTypeCode", "TypeDescription", propertyParameter.TypeOfInsurance);
            return View(propertyParameter);
        }

        // GET: PropertyParameters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyParameter = await _context.PropertyParameters.FindAsync(id);
            if (propertyParameter == null)
            {
                return NotFound();
            }
            ViewData["TypeOfInsurance"] = new SelectList(_context.Types_Of_Insurances, "InsuranceTypeCode", "TypeDescription", propertyParameter.TypeOfInsurance);
            return View(propertyParameter);
        }

        // POST: PropertyParameters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParamaterId,ParameterDescription,TypeOfInsurance")] PropertyParameter propertyParameter)
        {
            if (id != propertyParameter.ParamaterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyParameter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyParameterExists(propertyParameter.ParamaterId))
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
            ViewData["TypeOfInsurance"] = new SelectList(_context.Types_Of_Insurances, "InsuranceTypeCode", "TypeDescription", propertyParameter.TypeOfInsurance);
            return View(propertyParameter);
        }

        // GET: PropertyParameters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyParameter = await _context.PropertyParameters
                .Include(p => p.TypeOfInsuranceNavigation)
                .FirstOrDefaultAsync(m => m.ParamaterId == id);
            if (propertyParameter == null)
            {
                return NotFound();
            }

            return View(propertyParameter);
        }

        // POST: PropertyParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyParameter = await _context.PropertyParameters.FindAsync(id);
            _context.PropertyParameters.Remove(propertyParameter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyParameterExists(int id)
        {
            return _context.PropertyParameters.Any(e => e.ParamaterId == id);
        }
    }
}
