using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Course
{
    public class vm_update_course
    {
        public string? Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
    }
}
