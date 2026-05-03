using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        [HttpGet]
        [Authorize]
        async public Task<ActionResult> GetTeachers()
        {
            List<Teacher> teachers = _context.Teachers.ToList();
            return Ok(teachers);
        }
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
