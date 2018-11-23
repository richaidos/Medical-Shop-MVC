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
    [Route("api/Pharmacie")]
    [ApiController]
    public class APIPharmacieController : ControllerBase
    {
        private readonly MEDContext _context;

        public APIPharmacieController(MEDContext context)
        {
            _context = context;
        }

        // GET: api/Pharmacie
        [HttpGet]
        public IEnumerable<Pharmacy> GetPharmacy()
        {
            return _context.Pharmacy;
        }

        // GET: api/Pharmacie/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPharmacy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pharmacy = await _context.Pharmacy.FindAsync(id);

            if (pharmacy == null)
            {
                return NotFound();
            }

            return Ok(pharmacy);
        }

        // PUT: api/Pharmacie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPharmacy([FromRoute] int id, [FromBody] Pharmacy pharmacy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pharmacy.PharmID)
            {
                return BadRequest();
            }

            _context.Entry(pharmacy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PharmacyExists(id))
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

        // POST: api/Pharmacie
        [HttpPost]
        public async Task<IActionResult> PostPharmacy([FromBody] Pharmacy pharmacy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pharmacy.Add(pharmacy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPharmacy", new { id = pharmacy.PharmID }, pharmacy);
        }

        // DELETE: api/Pharmacie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePharmacy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pharmacy = await _context.Pharmacy.FindAsync(id);
            if (pharmacy == null)
            {
                return NotFound();
            }

            _context.Pharmacy.Remove(pharmacy);
            await _context.SaveChangesAsync();

            return Ok(pharmacy);
        }

        private bool PharmacyExists(int id)
        {
            return _context.Pharmacy.Any(e => e.PharmID == id);
        }
    }
}