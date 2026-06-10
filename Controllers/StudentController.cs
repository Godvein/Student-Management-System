using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // add student
        [HttpPost]
        [Authorize(Roles = "Admin")]
        async public Task<ActionResult> AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        // get all students with pagination
        [HttpGet]
        [Authorize]
        async public Task<ActionResult> GetStudents( int page = 1, int pageSize = 10)
        {
            // paginated method to get students
            List<Student> students = await _context.Students
                .Skip((page - 1) * pageSize)
                .Take(pageSize).
                ToListAsync();

            return Ok(students);
        }

        // get students by id
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

        // update student by id
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

        // delete student by id
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
