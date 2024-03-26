using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Message
{
	public class vm_create_message
	{
		public int ClassId { get; set; }
		public string? Content { get; set; }
		public Status Status { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
