using AutoMapper;
using BLL.ViewModels.Teacher_Class;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Teacher_Student_Mapping : Profile
    {
        public Teacher_Student_Mapping()
        {
            CreateMap<Teacher_Class, vm_teacher_class>().ReverseMap();
        }
    }
}
