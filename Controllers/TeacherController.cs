using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMS.Data;
using StudentMS.Models;

namespace StudentMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public TeacherController(DatabaseContext context)
        {
            _context = context;
        }

        // add teacher
        [HttpPost]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        // get all teachers with pagination
        [HttpGet]
        [Authorize]
        async public Task<ActionResult> GetTeachers( int page = 1, int pageSize = 10)
        {
            List<Teacher> teachers = await _context.Teachers.
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToListAsync();
            return Ok(teachers);
        }

        // get teacher by id
        [HttpGet("{id}")]
        [Authorize]
        async public Task<ActionResult> GetTeacher(int id)
        {
            Teacher? teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        // update teacher by id
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> UpdateTeacher(int id, Teacher updatedTeacher)
        {
            Teacher? teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            teacher.Name = updatedTeacher.Name;
            teacher.Email = updatedTeacher.Email;
            teacher.Course = updatedTeacher.Course;
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        // delete teacher by id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> DeleteTeacher(int id)
        {
            Teacher? teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
