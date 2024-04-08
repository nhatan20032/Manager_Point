using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Role
{
    public class vm_update_role
    {
        public string? Role_Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
    }
}
