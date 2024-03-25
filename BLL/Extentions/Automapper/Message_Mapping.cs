using AutoMapper;
using BLL.ViewModels.Message;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
	public class Message_Mapping : Profile
	{
		public Message_Mapping() 
		{
			CreateMap<Message, vm_message>().ReverseMap();
			CreateMap<Message, vm_create_message>().ReverseMap();
			CreateMap<Message, vm_update_message>().ReverseMap();
		}
	}
}
