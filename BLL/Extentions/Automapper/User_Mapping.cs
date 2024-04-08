﻿using AutoMapper;
using BLL.ViewModels.User;
using Manager_Point.Models;

namespace BLL.Extentions.Automapper
{
    public class User_Mapping : Profile
    {
        public User_Mapping()
        {
            CreateMap<User, vm_user>()
                .ForMember(
                    dest => dest.Role_User,
                    opt => opt.MapFrom(src => src.User_Roles!.Select(ur => ur.Role.Name).ToList())
                )
                .ForMember(
                    dest => dest.Subject_User,
                    opt => opt.MapFrom(src => src.Subject_Teachers!.Select(ur => ur.Subject.Name).ToList())
                )
                .ForMember(
                    dest => dest.Student_Class,
                    opt => opt.MapFrom(src => src.Student_Classes!.Select(ur => ur.Class.ClassCode).ToList())
                )
                .ForMember(
                    dest => dest.Teacher_Class,
                    opt => opt.MapFrom(src => src.Teacher_Classes!.Select(ur => ur.Class.ClassCode).ToList())
                )
                .ForMember(
                    dest => dest.Role_id,
                    opt => opt.MapFrom(src => src.User_Roles!.Select(ur => ur.Role.Id).ToList())
                )
                .ForMember(
                    dest => dest.Subject_id,
                    opt => opt.MapFrom(src => src.Subject_Teachers!.Select(ur => ur.Subject.Id).ToList())
                )
                .ForMember(
                    dest => dest.Student_Class_id,
                    opt => opt.MapFrom(src => src.Student_Classes!.Select(ur => ur.Class.Id).ToList())
                )
                .ForMember(
                    dest => dest.Teacher_Class_id,
                    opt => opt.MapFrom(src => src.Teacher_Classes!.Select(ur => ur.Class.Id).ToList())
                )
                .ForMember(
                    dest => dest.Role_Code,
                    opt => opt.MapFrom(src => src.User_Roles!.Select(ur => ur.Role.Role_Code).ToList())
                )
                .ReverseMap();
            CreateMap<User, vm_create_user>().ReverseMap();
            CreateMap<User, vm_update_user>().ReverseMap();
        }
    }
}
