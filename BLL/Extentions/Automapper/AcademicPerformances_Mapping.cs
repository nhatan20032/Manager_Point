using AutoMapper;
using BLL.ViewModels.AcademicPerformance;
using Data.Models;

namespace BLL.Extentions.Automapper
{
	public class AcademicPerformances_Mapping : Profile
	{
        public AcademicPerformances_Mapping()
        {
			CreateMap<AcademicPerformance, vm_academicperformance>().ReverseMap();
			CreateMap<AcademicPerformance, vm_create_academicperformance>().ReverseMap();
			CreateMap<AcademicPerformance, vm_update_academicperformance>().ReverseMap();
		}
    }
}
