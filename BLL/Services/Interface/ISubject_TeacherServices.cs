using BLL.ViewModels.Role;
using BLL.ViewModels.Subject_Teacher;

namespace BLL.Services.Interface
{
    public interface ISubject_TeacherServices
    {
        public Task<int> Create_Item(vm_subject_teacher request);
        public Task<List<int>> Batch_Create_Item(List<vm_subject_teacher> requests);
        public Task<int> Modified_Item(int id, vm_subject_teacher request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
    }
}
