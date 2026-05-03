namespace StudentMS.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<Role> Roles { get; set; } = new List<Role>();

        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set;}
    }
}
