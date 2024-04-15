using AutoMapper;
using BLL.ViewModels.Teacher_Class;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Teacher_Class_Mapping : Profile
    {
        public Teacher_Class_Mapping()
        {
            CreateMap<Teacher_Class, vm_teacher_class>().ReverseMap();
            CreateMap<Teacher_Class, vm_teacher_class_subject>().ReverseMap();
        }
    }
}
