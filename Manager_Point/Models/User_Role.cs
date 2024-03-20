namespace Manager_Point.Models
{
    public class User_Role
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
