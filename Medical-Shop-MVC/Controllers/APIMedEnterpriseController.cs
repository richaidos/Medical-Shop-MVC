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
    [Route("api/MedEnterprise")]
    [ApiController]
    public class APIMedEnterpriseController : ControllerBase
    {
        private readonly MEDContext _context;

        public APIMedEnterpriseController(MEDContext context)
        {
            _context = context;
        }

        // GET: api/MedEnterprise
        [HttpGet]
        public IEnumerable<Medical_Enterprise> GetMedical_Enterprise()
        {
            return _context.Medical_Enterprise;
        }

        // GET: api/MedEnterprise/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedical_Enterprise([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medical_Enterprise = await _context.Medical_Enterprise.FindAsync(id);

            if (medical_Enterprise == null)
            {
                return NotFound();
            }

            return Ok(medical_Enterprise);
        }

        // PUT: api/MedEnterprise/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedical_Enterprise([FromRoute] int id, [FromBody] Medical_Enterprise medical_Enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medical_Enterprise.MedID)
            {
                return BadRequest();
            }

            _context.Entry(medical_Enterprise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Medical_EnterpriseExists(id))
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

        // POST: api/MedEnterprise
        [HttpPost]
        public async Task<IActionResult> PostMedical_Enterprise([FromBody] Medical_Enterprise medical_Enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Medical_Enterprise.Add(medical_Enterprise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedical_Enterprise", new { id = medical_Enterprise.MedID }, medical_Enterprise);
        }

        // DELETE: api/MedEnterprise/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedical_Enterprise([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medical_Enterprise = await _context.Medical_Enterprise.FindAsync(id);
            if (medical_Enterprise == null)
            {
                return NotFound();
            }

            _context.Medical_Enterprise.Remove(medical_Enterprise);
            await _context.SaveChangesAsync();

            return Ok(medical_Enterprise);
        }

        private bool Medical_EnterpriseExists(int id)
        {
            return _context.Medical_Enterprise.Any(e => e.MedID == id);
        }
    }
}