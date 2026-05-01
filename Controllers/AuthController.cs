using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentMS.Data;
using StudentMS.DTOs.UserDTOs;
using StudentMS.Models;
using StudentMS.Services;

namespace StudentMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly JwtService _jwtService;

        public AuthController(DatabaseContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // login endpoint to authenticate user logic
        [HttpPost("login")]
        async public Task<ActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            if(dto == null)
            {
                return BadRequest("Invalid request data.");
            }


            // verify user credentials
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (user == null || !isPasswordValid)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new {token});
        }

        // register endpoint to create a new user logic
        [HttpPost("register")]
        async public Task<ActionResult> Register([FromBody] RegisterRequestDTO dto)
        { 
            if(dto == null)
            {
                return BadRequest("Invalid request data.");
            }

            // check if user exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            var HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Password = HashedPassword,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            return Ok(new {token});
        }
    }
}
