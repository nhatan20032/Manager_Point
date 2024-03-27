using BLL.ViewModels.Teacher_Class;

namespace BLL.Services.Interface
{
    public interface ITeacher_ClassServices
    {
        public Task<int> Create_Item(vm_teacher_class request);
        public Task<List<int>> Batch_Create_Item(List<vm_teacher_class> requests);
        public Task<int> Modified_Item(int id, vm_teacher_class request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
    }
}
