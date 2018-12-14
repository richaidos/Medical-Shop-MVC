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
    [Route("api/specialization")]
    [ApiController]
    public class APISpecializationController : ControllerBase
    {
        private readonly MEDContext _context;

        public APISpecializationController(MEDContext context)
        {
            _context = context;
        }

        // GET: api/specialization
        [HttpGet]
        public IEnumerable<Specialization> GetSpecialization()
        {
            return _context.Specialization;
        }

        // GET: api/specialization/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialization([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var specialization = await _context.Specialization.FindAsync(id);

            if (specialization == null)
            {
                return NotFound();
            }

            return Ok(specialization);
        }

        // PUT: api/specialization/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization([FromRoute] int id, [FromBody] Specialization specialization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != specialization.SpecID)
            {
                return BadRequest();
            }

            _context.Entry(specialization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecializationExists(id))
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

        // POST: api/specialization
        [HttpPost]
        public async Task<IActionResult> PostSpecialization([FromBody] Specialization specialization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Specialization.Add(specialization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialization", new { id = specialization.SpecID }, specialization);
        }

        // DELETE: api/specialization/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var specialization = await _context.Specialization.FindAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            _context.Specialization.Remove(specialization);
            await _context.SaveChangesAsync();

            return Ok(specialization);
        }

        private bool SpecializationExists(int id)
        {
            return _context.Specialization.Any(e => e.SpecID == id);
        }
    }
}