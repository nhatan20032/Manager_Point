using BLL.ViewModels.User;

namespace BLL.Author
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string? UserCode { get; set; }
        public List<int>? ClassId { get; set; }
        public List<string>? RoleCode { get; set; }
        public string? FullName { get; set; }
        public ICollection<string>? Role { get; set; }
        public string? Token { get; set; }

        public AuthenticateResponse(vm_user user, string token)
        {
            Id = user.Id;
            UserCode = user.User_Code;
            RoleCode = user.Role_Code;
            FullName = user.Name;
            ClassId = user.Student_Class_id;
            Role = user.Role_User;
            Token = token;
        }
    }
}
