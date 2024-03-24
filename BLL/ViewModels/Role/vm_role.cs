using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Role
{
    public class vm_role
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
    }
}
