﻿using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Subject
{
    public class vm_create_subject
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
    }
}
