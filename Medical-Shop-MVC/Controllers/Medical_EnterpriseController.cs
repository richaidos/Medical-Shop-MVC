using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medical_Shop_MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace Medical_Shop_MVC.Controllers
{
    
    public class Medical_EnterpriseController : Controller
    {
        private readonly MEDContext _context;

        public Medical_EnterpriseController(MEDContext context)
        {
            _context = context;
        }

        // GET: Medical_Enterprise
        public async Task<IActionResult> Index()
        {
            var medenterprisesize = _context.Medical_Enterprise.Count();
            ViewBag.medentercount = medenterprisesize;
            return View(await _context.Medical_Enterprise.ToListAsync());
        }

        // GET: Medical_Enterprise/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medical_Enterprise = await _context.Medical_Enterprise
                .FirstOrDefaultAsync(m => m.MedID == id);
            if (medical_Enterprise == null)
            {
                return NotFound();
            }

            return View(medical_Enterprise);
        }

        // GET: Medical_Enterprise/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medical_Enterprise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("MedID,MedName,MedDescription,MedAddress,Time_at")] Medical_Enterprise medical_Enterprise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medical_Enterprise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medical_Enterprise);
        }

        // GET: Medical_Enterprise/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medical_Enterprise = await _context.Medical_Enterprise.FindAsync(id);
            if (medical_Enterprise == null)
            {
                return NotFound();
            }
            return View(medical_Enterprise);
        }

        // POST: Medical_Enterprise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("MedID,MedName,MedDescription,MedAddress,Time_at")] Medical_Enterprise medical_Enterprise)
        {
            if (id != medical_Enterprise.MedID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medical_Enterprise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Medical_EnterpriseExists(medical_Enterprise.MedID))
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
            return View(medical_Enterprise);
        }

        // GET: Medical_Enterprise/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medical_Enterprise = await _context.Medical_Enterprise
                .FirstOrDefaultAsync(m => m.MedID == id);
            if (medical_Enterprise == null)
            {
                return NotFound();
            }

            return View(medical_Enterprise);
        }

        // POST: Medical_Enterprise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medical_Enterprise = await _context.Medical_Enterprise.FindAsync(id);
            _context.Medical_Enterprise.Remove(medical_Enterprise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Medical_EnterpriseExists(int id)
        {
            return _context.Medical_Enterprise.Any(e => e.MedID == id);
        }
    }
}
