using AutoMapper;
using BLL.ViewModels.GradePoint;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class GradePoint_Mapping : Profile
    {
        public GradePoint_Mapping()
        {
            CreateMap<GradePoint, vm_gradepoint>()
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.subjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.className, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(
                    dest => dest.ExaminationPoint,
                    opt => opt.MapFrom(src => src.Examinations!.Select(ur => ur.Point).ToList())
                )
                .ReverseMap();
            CreateMap<GradePoint, vm_create_gradepoint>().ReverseMap();
            CreateMap<GradePoint, vm_update_gradepoint>().ReverseMap();

        }

    }
}
