using BLL.Author;
using BLL.ViewModels.User;

namespace BLL.Services.Interface
{
    public interface IUserServices
    {
        public Task<string> Get_All_Async(int offset = 0, int limit = 10, string search = "");
        public Task<string> Get_User_No_Role(int offset = 0, int limit = 10, string search = "");
        public Task<string> Get_All_Teacher(int offset = 0, int limit = 10, string search = "", int subject = 0, int classes = 0, int check_subject = 0);
        public Task<string> Get_All_Teacher_No_HomeRoom(int offset = 0, int limit = 10, string search = "", int subject = 0, int classes = 0);
        public Task<string> Count_All_Teacher_Student();
        public Task<string> Count_Teachers_By_Subject();
        public Task<string> Count_Students_By_Course();
        public Task<string> Count_Student_In_Year();
        public Task<string> Count_Students_By_Class();
        public Task<string> Count_All_Rank_Student();
        public Task<string> Count_All_Rank_Student_Year();
        public Task<string> Count_All_Rank_Student_GradeLevel();
        public Task<string> Get_All_Student(int offset = 0, int limit = 10, string search = "", int classes = 0, int check_class = 0);
        public Task<vm_user> Get_By_Id(int id);
        public Task<vm_teacher> Get_By_HomeRoom_Id(int idClass);
        public Task<string> Get_By_Subject_Teacher_Id(int idClass);
        public Task<int> Create_Item(vm_create_user request);
        public Task<List<int>> Batch_Create_Item(List<vm_create_user> requests);
        public Task<int> Modified_Item(int id, vm_update_user request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
        public Task<int> AddUsersFromExcel(Stream excelFileStream);
        public AuthenticateResponse? Authenticate(AuthenticateRequest model);
    }
}
