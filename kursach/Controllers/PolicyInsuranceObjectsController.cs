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
    public class PolicyInsuranceObjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicyInsuranceObjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PolicyInsuranceObjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PolicyInsuranceObjects.Include(p => p.Parameter).Include(p => p.Policy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PolicyInsuranceObjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyInsuranceObject = await _context.PolicyInsuranceObjects
                .Include(p => p.Parameter)
                .Include(p => p.Policy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policyInsuranceObject == null)
            {
                return NotFound();
            }

            return View(policyInsuranceObject);
        }

        // GET: PolicyInsuranceObjects/Create
        public IActionResult Create()
        {
            ViewData["ParameterId"] = new SelectList(_context.PropertyParameters, "ParamaterId", "ParameterDescription");
            ViewData["PolicyId"] = new SelectList(_context.Policies, "PolicyId", "PolicyId");
            return View();
        }

        // POST: PolicyInsuranceObjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PolicyId,ParameterId,ParamenterValue")] PolicyInsuranceObject policyInsuranceObject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyInsuranceObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParameterId"] = new SelectList(_context.PropertyParameters, "ParamaterId", "ParameterDescription", policyInsuranceObject.ParameterId);
            ViewData["PolicyId"] = new SelectList(_context.Policies, "PolicyId", "PolicyId", policyInsuranceObject.PolicyId);
            return View(policyInsuranceObject);
        }

        // GET: PolicyInsuranceObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyInsuranceObject = await _context.PolicyInsuranceObjects.FindAsync(id);
            if (policyInsuranceObject == null)
            {
                return NotFound();
            }
            ViewData["ParameterId"] = new SelectList(_context.PropertyParameters, "ParamaterId", "ParameterDescription", policyInsuranceObject.ParameterId);
            ViewData["PolicyId"] = new SelectList(_context.Policies, "PolicyId", "PolicyId", policyInsuranceObject.PolicyId);
            return View(policyInsuranceObject);
        }

        // POST: PolicyInsuranceObjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PolicyId,ParameterId,ParamenterValue")] PolicyInsuranceObject policyInsuranceObject)
        {
            if (id != policyInsuranceObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyInsuranceObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyInsuranceObjectExists(policyInsuranceObject.Id))
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
            ViewData["ParameterId"] = new SelectList(_context.PropertyParameters, "ParamaterId", "ParameterDescription", policyInsuranceObject.ParameterId);
            ViewData["PolicyId"] = new SelectList(_context.Policies, "PolicyId", "PolicyId", policyInsuranceObject.PolicyId);
            return View(policyInsuranceObject);
        }

        // GET: PolicyInsuranceObjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyInsuranceObject = await _context.PolicyInsuranceObjects
                .Include(p => p.Parameter)
                .Include(p => p.Policy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policyInsuranceObject == null)
            {
                return NotFound();
            }

            return View(policyInsuranceObject);
        }

        // POST: PolicyInsuranceObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyInsuranceObject = await _context.PolicyInsuranceObjects.FindAsync(id);
            _context.PolicyInsuranceObjects.Remove(policyInsuranceObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyInsuranceObjectExists(int id)
        {
            return _context.PolicyInsuranceObjects.Any(e => e.Id == id);
        }
    }
}
