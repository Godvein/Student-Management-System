namespace StudentMS.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
