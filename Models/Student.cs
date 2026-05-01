using System.ComponentModel.DataAnnotations;

namespace StudentMS.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Course { get; set; } = string.Empty;
        public Teacher ? teacher { get; set; }

    }
}
