using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Class
{
    public class vm_class
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public required string ClassCode { get; set; }
        public string? Name { get; set; }
        public int GradeLevel { get; set; }
        public Status Status { get; set; } 
        public string CourseName { get; set; }
    }
}
