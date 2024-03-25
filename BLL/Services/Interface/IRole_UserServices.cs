using BLL.ViewModels.Role_User;

namespace BLL.Services.Interface
{
    public interface IRole_UserServices
    {
        public Task<int> Create_Item(vm_role_user request);
        public Task<List<int>> Batch_Create_Item(List<vm_role_user> requests);
        public Task<int> Modified_Item(int id, vm_role_user request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
    }
}
