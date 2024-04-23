using AutoMapper;
using BLL.ViewModels.Class;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Class_Mapping : Profile
    {
        public Class_Mapping()
        {
            CreateMap<Class, vm_class>().ForMember(dest => dest.CourseName, opt => opt.MapFrom(srs => srs.Course.Name)).ReverseMap();
            CreateMap<Class, vm_create_class>().ReverseMap();
            CreateMap<Class, vm_update_class>().ReverseMap();

        }

    }
}
