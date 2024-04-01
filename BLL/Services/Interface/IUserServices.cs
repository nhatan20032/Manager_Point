using BLL.Author;
using BLL.ViewModels.User;

namespace BLL.Services.Interface
{
    public interface IUserServices
    {
        public Task<string> Get_All_Async(int offset = 0, int limit = 10, string search = "");
        public Task<string> Get_All_Teacher(int offset = 0, int limit = 10, string search = "", int role = 1, int subject = 0, int classes = 0);
        public Task<string> Get_All_Student(int offset = 0, int limit = 10, string search = "", int role = 2, int classes = 0);
        public Task<vm_user> Get_By_Id(int id);
        public Task<int> Create_Item(vm_create_user request);
        public Task<List<int>> Batch_Create_Item(List<vm_create_user> requests);
        public Task<int> Modified_Item(int id, vm_update_user request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
        public Task<int> AddUsersFromExcel(Stream excelFileStream);
        public AuthenticateResponse? Authenticate(AuthenticateRequest model);
    }
}
