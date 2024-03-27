using BLL.ViewModels.Student_Class;

namespace BLL.Services.Interface
{
    public interface IStudent_ClassServices
    {
        public Task<int> Create_Item(vm_student_class request);
        public Task<List<int>> Batch_Create_Item(List<vm_student_class> requests);
        public Task<int> Modified_Item(int id, vm_student_class request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
    }
}
