using AutoMapper;
using BLL.ViewModels.GradePoint;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
	public class GradePoint_Mapping : Profile
	{
		public GradePoint_Mapping()
		{
			CreateMap<GradePoint, vm_gradepoint>().ReverseMap();
			CreateMap<GradePoint, vm_create_gradepoint>().ReverseMap();
			CreateMap<GradePoint, vm_update_gradepoint>().ReverseMap();

		}

	}
}
