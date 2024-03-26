using AutoMapper;
using BLL.ViewModels.User;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class User_Mapping : Profile
    {
        public User_Mapping()
        {
            CreateMap<User, vm_user>()
                .ForMember(
                    dest => dest.Role_User,
                    opt => opt.MapFrom(src => src.User_Roles!.Select(ur => ur.Role.Name).ToList())
                )
                .ForMember(
                    dest => dest.Subject_User,
                    opt => opt.MapFrom(src => src.Subject_Teachers!.Select(ur => ur.Subject.Name).ToList())
                )
                .ForMember(
                    dest => dest.User_Class,
                    opt => opt.MapFrom(src => src.Student_Classes!.Select(ur => ur.Class.Name).ToList())
                )
                .ReverseMap();
            CreateMap<User, vm_create_user>().ReverseMap();
            CreateMap<User, vm_update_user>().ReverseMap();
        }
    }
}
