using BLL.ViewModels.Teacher_Class;

namespace BLL.Services.Interface
{
    public interface ITeacher_ClassServices
    {
        public Task<int> Create_Item(vm_teacher_class request);
        public Task<string> Batch_Create_Item_Subject(List<vm_teacher_class_subject> requests);
        public Task<string> Batch_Create_Item_HomeRoom(List<vm_teacher_class> requests);
        public Task<int> Modified_Item(int id, vm_teacher_class request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item_HomeRoom(int idClass);
        public Task<bool> Batch_Remove_Item_Subject(int UserId);
        public Task<bool> Batch_Remove_Item(List<int> ids);
    }
}
