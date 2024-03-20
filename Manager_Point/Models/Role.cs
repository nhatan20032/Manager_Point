using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
    }
}
