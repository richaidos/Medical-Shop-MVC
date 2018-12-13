using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Medical_Shop_MVC.Models;

namespace Medical_Shop_MVC.Controllers
{
    [Route("api/Doctor")]
    [ApiController]
    public class APIDoctorsController : ControllerBase
    {
        private readonly MEDContext _context;

        public APIDoctorsController(MEDContext context)
        {
            _context = context;
        }

        // GET: api/Doctor
        [HttpGet]
        public IEnumerable<Doctors> GetDoctors()
        {
            return _context.Doctors
                .Include(e => e.doctorType)
                .Include(d => d.DoctorEnterprise);
        }

        // GET: api/Doctor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctors([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var doctors = await _context.Doctors.FindAsync(id);
            var doctors = await _context.Doctors
                .Include(e => e.doctorType)
                .Include(d => d.DoctorEnterprise)
                .FirstOrDefaultAsync(e => e.DoctorID == id);

            if (doctors == null)
            {
                return NotFound();
            }

            return Ok(doctors);
        }

        // PUT: api/Doctor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctors([FromRoute] int id, [FromBody] Doctors doctors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctors.DoctorID)
            {
                return BadRequest();
            }

            _context.Entry(doctors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Doctor
        [HttpPost]
        public async Task<IActionResult> PostDoctors([FromBody] Doctors doctors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = Int32.Parse(Request.Form["doctorType"]);
            var id2 = Int32.Parse(Request.Form["DoctorEnterprise"]);
            //System.Diagnostics.Debug.WriteLine("ID1: "+id+" ID2: "+id2);
            var specialization = await _context.Specialization.FindAsync(id);
            var med = await _context.Medical_Enterprise.FindAsync(id2);
            doctors.doctorType = specialization;
            doctors.DoctorEnterprise = med;

            _context.Doctors.Add(doctors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctors", new { id = doctors.DoctorID }, doctors);
        }

        // DELETE: api/Doctor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctors([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctors = await _context.Doctors.FindAsync(id);
            if (doctors == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctors);
            await _context.SaveChangesAsync();

            return Ok(doctors);
        }

        private bool DoctorsExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorID == id);
        }
    }
}