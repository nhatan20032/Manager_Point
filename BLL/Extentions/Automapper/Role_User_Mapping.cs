using AutoMapper;
using BLL.ViewModels.Role_User;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Role_User_Mapping : Profile
    {
        public Role_User_Mapping()
        {
            CreateMap<User_Role, vm_role_user>().ReverseMap();
        }
    }
}
