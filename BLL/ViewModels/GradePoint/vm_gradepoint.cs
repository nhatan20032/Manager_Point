using Manager_Point.Models.Enum;

namespace BLL.ViewModels.GradePoint
{
    public class vm_gradepoint
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string? subjectName { get; set; }
        public string? userName { get; set; }
        public List<float>? ExaminationPoint { get; set; }
        public string? className { get; set; }
        public Semester Semester { get; set; }
        public float Midterm_Grades { get; set; }
        public float Final_Grades { get; set; }
        public float Average
        {
            get { return (float)Math.Round(_average, 1); }
            set { _average = value; }
        }
        private float _average;


    }
}
