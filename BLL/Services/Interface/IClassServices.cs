using BLL.ViewModels.Class;
using BLL.ViewModels.Teacher_Class;

namespace BLL.Services.Interface
{
    public interface IClassServices
    {
        public Task<string> Get_All_Async(int offset = 0, int limit = 10, string search = "");
        public List<vm_class> Get_List();
        public Task<string> Get_By_Id(int id);
        public Task<vm_class> Get_By_Id_vm_class(int id);
        public Task<string> GetClassOnBoard(int idUser);
        public Task<string> GetHomeRoomOnBoard(int idUser);
        public Task<int> Create_Item(vm_create_class request);
        public Task<List<int>> Batch_Create_Item(List<vm_create_class> requests);
        public Task<int> Modified_Item(int id, vm_update_class request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);

        public Task<string> GradePointByClass(int idClass, int? semester = null);
        public Task<string> GradePointByClassAllYear(int idClass, int? semester = null);
        public Task<string> GradePointSubjectByClass(int idClass, int idUser, int? semester = null);

    }
}
