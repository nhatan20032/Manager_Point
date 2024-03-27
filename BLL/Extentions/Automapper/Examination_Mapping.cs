using AutoMapper;
using BLL.ViewModels.Examination;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class Examination_Mapping : Profile
    {
        public Examination_Mapping()
        {
            CreateMap<Examination, vm_examination>().ReverseMap();
            CreateMap<Examination, vm_create_examination>().ReverseMap();
            CreateMap<Examination, vm_update_examination>().ReverseMap();
        }
    }
}
