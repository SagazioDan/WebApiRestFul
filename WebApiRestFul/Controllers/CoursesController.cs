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
    [Route("api/Courses")]
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        /// <summary>
        /// Mostra tutti i corsi.
        /// </summary>
        [HttpGet]
        public ActionResult GetCourses()
        {
            return Ok(_context.Courses);
        }

        //GET: api/Courses/5
        /// <summary>
        /// Mostra corsi in base all'ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.ID == id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }


        //GET: api/courses/title/matematica
        /// <summary>
        /// Mostra corsi in base al titolo del corso.
        /// </summary>
        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetCourseByTitleAsync([FromRoute] string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.Where(m => m.Title == title).ToListAsync();

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Courses/5
        /// <summary>
        /// Modifica corsi in base all'ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseAsync([FromRoute] int id, [FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.ID)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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


        // POST: api/Courses
        /// <summary>
        /// Aggiunge corsi in base all'ID.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Course
        ///     {
        ///        "id": 4,
        ///        "title": "informatica",
        ///        "credits":  "Nesi"
        ///     }
        ///
        /// </remarks>
        /// <param name="course"></param>
        /// <returns>A newly-created CourseItem</returns>
        [HttpPost]
        public async Task<IActionResult> PostCourseAsync([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            CreatedAtAction("GetCourse", new { id = course.ID }, course);
            return Ok(ModelState);
        }

        // DELETE: api/Courses/5
        /// <summary>
        /// Cancella corsi in base all'ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.ID == id);
        }
    }
}