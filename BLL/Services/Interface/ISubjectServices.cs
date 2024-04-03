using BLL.ViewModels.Subject;

namespace BLL.Services.Interface
{
    public interface ISubjectServices
    {
        public Task<string> Get_All_Async(int offset = 0, int limit = 10, string search = "");
        public List<vm_subject> Get_List();
        public Task<vm_subject> Get_By_Id(int id);
        public Task<int> Create_Item(vm_create_subject request);
        public Task<List<int>> Batch_Create_Item(List<vm_create_subject> requests);
        public Task<int> Modified_Item(int id, vm_update_subject request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
    }
}
