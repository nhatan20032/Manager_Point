using BLL.ViewModels.GradePoint;

namespace BLL.Services.Interface
{
    public interface IGradePointServices
    {
        public Task<string> Get_All_Async(int page_number = 1, int page_size = 10, string search = "");
        public Task<vm_gradepoint> Get_By_Id(int id);
        public Task<int> Create_Item(vm_create_gradepoint request);
        public Task<List<int>> Batch_Create_Item(List<vm_create_gradepoint> requests);
        public Task<int> Modified_Item(int id, vm_update_gradepoint request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
        public Task<int> ImportFromExcel(Stream excelFileStream);
    }
}
