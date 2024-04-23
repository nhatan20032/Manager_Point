namespace BLL.ViewModels.GradePoint
{
    public class vm_gradepoint_whole_year
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public float Midterm_Grades_Whole_year { get; set; }
        public float Final_Grades_Whole_year { get; set; }

        private float _averageWholeYear;

        public float Average_Whole_year
        {
            get { return (float)Math.Round(_averageWholeYear, 1); }
            set { _averageWholeYear = value; }
        }


    }
}
