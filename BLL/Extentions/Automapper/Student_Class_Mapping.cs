using AutoMapper;
using BLL.ViewModels.Student_Class;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Student_Class_Mapping : Profile
    {
        public Student_Class_Mapping()
        {
            CreateMap<Student_Class, vm_student_class>().ReverseMap();
        }
    }
}
