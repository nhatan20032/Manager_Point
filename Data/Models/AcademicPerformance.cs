using Data.Models.Enum;
using Manager_Point.Models;
using Manager_Point.Models.Enum;

namespace Data.Models
{
    public class AcademicPerformance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GradePointId { get; set; }
        public Performance Performance { get; set; }
        public Status Status { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual GradePoint GradePoint { get; set; } = null!;

    }
}
