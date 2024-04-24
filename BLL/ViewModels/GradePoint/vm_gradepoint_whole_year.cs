using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.GradePoint
{
    public class vm_gradepoint_whole_year
    {
        public string UserName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public float Semester1 { get; set; }
        public float Semester2 { get; set; }

        private float _averageWholeYear;

        public float Average_Whole_year
        {
            get { return (float)Math.Round(_averageWholeYear, 1); }
            set { _averageWholeYear = value; }
        }


    }
}
