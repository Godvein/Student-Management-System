using StudentMS.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentMS.DTOs.UserDTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
