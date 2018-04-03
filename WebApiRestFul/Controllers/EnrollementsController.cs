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
        /// <summary>
        /// Mostra tutte le Icrizioni.
        /// </summary>
        [HttpGet]
        public IEnumerable<Enrollement> GetEnrollements()
        {
            return _context.Enrollements;
        }

        // GET: api/Enrollements/5
        /// <summary>
        /// Mostra iscrizione in base all'ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollementAsync([FromRoute] int id)
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

        // GET api/Enrollements/all/IDstudent
        /// <summary>
        /// Mostra iscrizione in base all'ID dello studente.
        /// </summary>
        [HttpGet("all/{IDstudent}")]
        public async Task<IActionResult> GetEnrollementByStudentID([FromRoute] int IDstudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollements = await _context.Enrollements.Where(m => m.StudentID == IDstudent).ToListAsync();
           
            if(enrollements == null)
            {
                return NotFound();
            }

            return Ok(enrollements);

        }


        // PUT: api/Enrollements/5
        /// <summary>
        /// Modifica iscrizione in base all'ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollementAsync([FromRoute] int id, [FromBody] Enrollement enrollement)
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
        /// <summary>
        /// Aggiunge iscrizione in base all'ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///    POST /Enrollements
        ///     {
        ///        "EnrlomentsID": 5,
        ///        "StudentID": 2,
        ///        "CourseID": 5,
        ///        "Grade" : 1
        ///     }
        ///
        /// </remarks>
        /// <param name="course"></param>
        /// <returns>A newly-created CourseItem</returns>
        [HttpPost]
        public async Task<IActionResult> PostEnrollementAsync([FromBody] Enrollement enrollement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Enrollements.Add(enrollement);
            await _context.SaveChangesAsync();

            CreatedAtAction("GetEnrollement", new { id = enrollement.EnrollementID }, enrollement);
            return Ok(ModelState);

        }

        // DELETE: api/Enrollements/5
        /// <summary>
        /// Cancella iscrizione in base all'ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollementAsync([FromRoute] int id)
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