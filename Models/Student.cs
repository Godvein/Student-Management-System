namespace StudentMS.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public Teacher ? teacher { get; set; }

    }
}
