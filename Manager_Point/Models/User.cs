using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string User_Code { get; set; }
        public required string Password { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Address { get; set; }
        public string? Hometown { get; set; }
        public Status Status { get; set; }
    }
}
