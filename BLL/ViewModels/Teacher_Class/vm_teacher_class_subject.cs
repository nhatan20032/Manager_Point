using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Teacher_Class
{
    public class vm_teacher_class_subject
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public TypeTeacher TypeTeacher { get; set; }
    }
}
