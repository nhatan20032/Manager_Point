﻿using Manager_Point.Models.Enum;

namespace BLL.ViewModels.User
{
    public class vm_create_user
    {
        public required string User_Code { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Address { get; set; }
        public string? Nation { get; set; }
        public Status Status { get; set; }
    }
}
