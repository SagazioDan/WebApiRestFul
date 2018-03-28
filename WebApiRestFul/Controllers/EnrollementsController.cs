using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GlobalCityManager.Models;

namespace WebApiRestFul.Controllers
{
    [Produces("application/json")]
    [Route("api/Enrollements")]
    public class EnrollementsController : Controller
    {
        private readonly SchoolContext _context;

        public EnrollementsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Enrollements
        [HttpGet]
        public IEnumerable<Enrollement> GetEnrollements()
        {
            return _context.Enrollements;
        }

        // GET: api/Enrollements/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollement = await _context.Enrollements.SingleOrDefaultAsync(m => m.EnrollementID == id);

            if (enrollement == null)
            {
                return NotFound();
            }

            return Ok(enrollement);
        }

        // PUT: api/Enrollements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollement([FromRoute] int id, [FromBody] Enrollement enrollement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollement.EnrollementID)
            {
                return BadRequest();
            }

            _context.Entry(enrollement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollementExists(id))
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

        // POST: api/Enrollements
        [HttpPost]
        public async Task<IActionResult> PostEnrollement([FromBody] Enrollement enrollement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Enrollements.Add(enrollement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollement", new { id = enrollement.EnrollementID }, enrollement);
        }

        // DELETE: api/Enrollements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollement([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollement = await _context.Enrollements.SingleOrDefaultAsync(m => m.EnrollementID == id);
            if (enrollement == null)
            {
                return NotFound();
            }

            _context.Enrollements.Remove(enrollement);
            await _context.SaveChangesAsync();

            return Ok(enrollement);
        }

        private bool EnrollementExists(int id)
        {
            return _context.Enrollements.Any(e => e.EnrollementID == id);
        }
    }
}