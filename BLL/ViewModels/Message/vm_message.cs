using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Message
{
	public class vm_message
	{
		public int Id { get; set; }
		public int ClassId { get; set; }
		public string? Content { get; set; }
		public Status Status { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
