using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class Teacher_Class
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }     
        public TypeTeacher TypeTeacher { get; set; }
    }
}
