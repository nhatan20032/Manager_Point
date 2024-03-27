using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Message
{
    public class vm_update_message
    {
        public int ClassId { get; set; }
        public string? Content { get; set; }
        public Status Status { get; set; }

    }
}
