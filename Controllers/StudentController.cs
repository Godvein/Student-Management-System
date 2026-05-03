using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMS.Data;
using StudentMS.Models;

namespace StudentMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public StudentController(DatabaseContext context) { 
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> GetStudents()
        {
            List<Student> students = _context.Students.ToList();
            return Ok(students);
        }

        [HttpGet("{id}")]
        [Authorize]
        async public Task<ActionResult> GetStudent(int id)
        {
            Student? student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> UpdateStudent(int id, Student updatedStudent)
        {
            Student? student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = updatedStudent.Name;
            student.Email = updatedStudent.Email;
            student.Course = updatedStudent.Course;
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> DeleteStudent(int id)
        {
            Student? student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }
    }
}
