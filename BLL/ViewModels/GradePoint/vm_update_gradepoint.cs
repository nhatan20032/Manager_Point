using Manager_Point.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.GradePoint
{
	public class vm_update_gradepoint
	{
		public int SubjectId { get; set; }
		public int UserId { get; set; }
		public int ClassId { get; set; }
		public Semester Semester { get; set; }
		public float Midterm_Grades { get; set; }
		public float Final_Grades { get; set; }
		public float Average { get; set; }
	}
}
