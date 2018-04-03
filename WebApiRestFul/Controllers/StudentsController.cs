using GlobalCityManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRestFul.Controllers
{
    [Produces("application/json")]
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext _context)
        {
            context = _context;
        }

        // GET: api/Students
        /// <summary>
        /// Mostra tutti gli studenti.
        /// </summary>
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(context.Students);
        }

        // GET: api/Students/5
        /// <summary>
        /// Mostra studente in base all'ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await context.Students.SingleOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        /// <summary>
        /// Modifica studente in base all'ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentAsync([FromRoute] int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.ID)
            {
                return BadRequest();
            }

            context.Entry(student).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        /// <summary>
        /// Aggiunge studente.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Student
        ///     {
        ///        "ID": 0,
        ///        "FirstMidName": "Mario",
        ///        "LastName": "Rossi",
        ///     }
        ///
        /// </remarks>
        /// <param name="student"></param>
        /// <returns>A newly-created CourseItem</returns>
        [HttpPost]
        public async Task<IActionResult> PostStudentAsync([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Students.Add(student);
            await context.SaveChangesAsync();

            CreatedAtAction("GetStudent", new { id = student.ID }, student);
            return Ok(ModelState);

        }

        // DELETE: api/Students/5
        /// <summary>
        /// Canecella studente in base all'ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await context.Students.SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return Ok(student);
        }

        private bool StudentExists(int id)
        {
            return context.Students.Any(e => e.ID == id);
        }
    }
}