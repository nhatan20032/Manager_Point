namespace Manager_Point.Models
{
    public class GradePoint
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int UserId { get; set; }
        public string? Semester { get; set; }
        public float Midterm_Grades { get; set; }
        public float Final_Grades { get; set; }
        public float Average { get; set; }
    }
}
