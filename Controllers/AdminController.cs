using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using StudentMS.Data;
namespace StudentMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AssignRoles(int UserId, int RoleId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var role = await _context.Roles.FindAsync(RoleId);
            if (role == null)
            {
                return NotFound("Role not found");
            }
            user.Roles.Add(role);
            await _context.SaveChangesAsync();
            return Ok("Role assigned successfully");
        }

        [HttpPost("remove-role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoveRoles(int UserId, int RoleId)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var role = await _context.Roles.FindAsync(RoleId);
            if (role == null)
            {
                return NotFound("Role not found");
            }
            user.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return Ok("Role removed successfully");
        }
    }
}
