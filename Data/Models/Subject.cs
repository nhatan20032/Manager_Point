﻿using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }

        public virtual ICollection<Subject_Teacher>? Subject_Teachers { get; set; }
        public virtual ICollection<GradePoint>? GradePoints { get; set; }
    }
}
