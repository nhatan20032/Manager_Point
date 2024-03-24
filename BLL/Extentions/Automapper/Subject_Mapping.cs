using AutoMapper;
using BLL.ViewModels.Subject;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Subject_Mapping : Profile
    {
        public Subject_Mapping()
        {
            CreateMap<Subject, vm_subject>().ReverseMap();
            CreateMap<Subject, vm_create_subject>().ReverseMap();
            CreateMap<Subject, vm_update_subject>().ReverseMap();
        }
    }
}
