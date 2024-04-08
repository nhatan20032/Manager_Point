using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Role_Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }

        public virtual ICollection<User_Role>? User_Roles { get; set; }
    }
}
