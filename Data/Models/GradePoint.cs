using Data.Models;
using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class GradePoint
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public Semester Semester { get; set; }
        public float Midterm_Grades { get; set; }
        public float Final_Grades { get; set; }
        public float Average { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual ICollection<Examination>? Examinations { get; set; }
        public virtual ICollection<AcademicPerformance>? AcademicPerformances { get; set; }
    }
}
