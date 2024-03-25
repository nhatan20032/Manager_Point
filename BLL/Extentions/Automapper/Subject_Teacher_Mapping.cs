using AutoMapper;
using BLL.ViewModels.Subject_Teacher;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Subject_Teacher_Mapping : Profile
    {
        public Subject_Teacher_Mapping()
        {
            CreateMap<Subject_Teacher, vm_subject_teacher>().ReverseMap();
        }
    }
}
