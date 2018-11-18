using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medical_Shop_MVC.Models;

namespace Medical_Shop_MVC.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly MEDContext _context;
 
        public DoctorsController(MEDContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doctors.ToListAsync());
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var doctors = await _context.Doctors
                .FirstOrDefaultAsync(m => m.DoctorID == id);*/
            //Doctors doctors = await _context.Doctors.FindAsync(id);
            var doctors = await _context.Doctors
                .Include(e => e.doctorType)
                .Include(d => d.DoctorEnterprise)
                .FirstOrDefaultAsync(e => e.DoctorID == id);

            if (doctors == null)
            {
                return NotFound();
            }

            return View(doctors);
        }

        // GET: Doctors/Create
        public async Task<IActionResult> Create()
        {
            var specializationList = await _context.Specialization.ToListAsync();
            var enterpriseList = await _context.Medical_Enterprise.ToListAsync();
            ViewBag.spList = specializationList;
            ViewBag.enList = enterpriseList;
            /*
            IEnumerable<SelectListItem> items = _context.Specialization.Select(c => new SelectListItem
            {
                Value = c.SpecID.ToString(),
                Text = c.SpecName

                //@Html.DropDownList("JobTitle", "Select a Value")
            });
            ViewBag.JobTitle = items;*/

            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorID,DoctorName,DoctorSurname,DoctorDescription,DoctorPhone,Price,doctorType,DoctorEnterprise")] Doctors doctors)
        {
            
            var id = Int32.Parse(Request.Form["doctorType"]);
            var id2 = Int32.Parse(Request.Form["DoctorEnterprise"]); 
            //System.Diagnostics.Debug.WriteLine("ID1: "+id+" ID2: "+id2);
            var specialization = await _context.Specialization.FindAsync(id);
            var med = await _context.Medical_Enterprise.FindAsync(id2);
            doctors.doctorType = specialization;
            doctors.DoctorEnterprise = med;
            
            if (ModelState.IsValid)
            {
                _context.Add(doctors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctors);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var specializationList = await _context.Specialization.ToListAsync();
            var enterpriseList = await _context.Medical_Enterprise.ToListAsync();
            ViewBag.spList = specializationList;
            ViewBag.enList = enterpriseList;

            if (id == null)
            {
                return NotFound();
            }

            var doctors = await _context.Doctors.FindAsync(id);
            if (doctors == null)
            {
                return NotFound();
            }
            
            return View(doctors);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorID,DoctorName,DoctorSurname,DoctorDescription,DoctorPhone,Price,doctorType,DoctorEnterprise")] Doctors doctors)
        {
            if (id != doctors.DoctorID)
            {
                return NotFound();
            }

            var id1 = Int32.Parse(Request.Form["doctorType"]);
            var id2 = Int32.Parse(Request.Form["DoctorEnterprise"]);
            var specialization = await _context.Specialization.FindAsync(id1);
            var med = await _context.Medical_Enterprise.FindAsync(id2);
            doctors.doctorType = specialization;
            doctors.DoctorEnterprise = med;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorsExists(doctors.DoctorID))
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
            return View(doctors);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctors = await _context.Doctors
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctors == null)
            {
                return NotFound();
            }

            return View(doctors);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctors = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorsExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorID == id);
        }
    }
}
