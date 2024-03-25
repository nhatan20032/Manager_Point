using AutoMapper;
using BLL.ViewModels.Class;
using BLL.ViewModels.Role;
using Manager_Point.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extentions.Automapper
{
	public class Class_Mapping : Profile
	{
		public Class_Mapping()
		{
			CreateMap<Class, vm_class>().ReverseMap();
			CreateMap<Class, vm_create_class>().ReverseMap();
			CreateMap<Class, vm_update_class>().ReverseMap();

		}

	}
}
