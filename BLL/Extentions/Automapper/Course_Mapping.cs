using AutoMapper;
using BLL.ViewModels.Course;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Course_Mapping : Profile
    {
        public Course_Mapping()
        {
            CreateMap<Course, vm_course>().ReverseMap();
            CreateMap<Course, vm_create_course>().ReverseMap();
            CreateMap<Course, vm_update_course>().ReverseMap();
        }
    }
}
