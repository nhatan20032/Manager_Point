using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class Class
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public required string ClassCode { get; set; }
        public string? Name { get; set; }
        public int GradeLevel { get; set; }
        public Status Status { get; set; }


        public virtual ICollection<Message>? Messages { get; set; }
        public virtual ICollection<Teacher_Class>? Teacher_Classes { get; set; }
        public virtual ICollection<Student_Class>? Student_Classes { get; set; }
        public virtual Course Course { get; set; } = null!;
    }
}
