using Manager_Point.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Class
{
	public class vm_update_class
	{
		public int CourseId { get; set; }
		public required string ClassCode { get; set; }
		public string? Name { get; set; }
		public int GradeLevel { get; set; }
		public Status Status { get; set; }

	}
}
