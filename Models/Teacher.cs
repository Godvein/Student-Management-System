using System.ComponentModel.DataAnnotations;

namespace StudentMS.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Course { get; set; } = string.Empty;
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
