﻿using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }

        public virtual ICollection<Class> Classes { get; set; } = null!;
    }
}
