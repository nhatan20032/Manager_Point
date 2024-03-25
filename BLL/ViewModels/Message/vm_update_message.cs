using Manager_Point.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Message
{
	public class vm_update_message
	{
		public int ClassId { get; set; }
		public string? Content { get; set; }
		public Status Status { get; set; }

	}
}
