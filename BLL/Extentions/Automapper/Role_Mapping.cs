using AutoMapper;
using BLL.ViewModels.Role;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Role_Mapping : Profile
    {
        public Role_Mapping()
        {
            CreateMap<Role, vm_role>().ReverseMap();
            CreateMap<Role, vm_create_role>().ReverseMap();
            CreateMap<Role, vm_update_role>().ReverseMap();
        }
    }
}
