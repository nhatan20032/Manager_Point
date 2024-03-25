using BLL.ViewModels.Course;

namespace BLL.Services.Interface
{
	public interface ICourseService
	{
		public Task<string> Get_All_Async(int page_number = 1, int page_size = 10, string search = "");
		public Task<vm_course> Get_By_Id(int id);
		public Task<int> Create_Item(vm_create_course request);
		public Task<List<int>> Batch_Create_Item(List<vm_create_course> requests);
		public Task<int> Modified_Item(int id, vm_update_course request);
		public Task<bool> Remove_Item(int id);
		public Task<bool> Batch_Remove_Item(List<int> ids);
	}
}
