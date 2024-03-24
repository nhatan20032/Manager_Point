using Data.Models.Enum;
using Manager_Point.Models;
using Manager_Point.Models.Enum;

namespace Data.Models
{
    public class AcademicPerformance
    {
        public int Id { get; set; }
        public int GradePointId { get; set; }
        public Performance Performance { get; set; }
        public Status Status { get; set; }

        public virtual GradePoint GradePoint { get; set; } = null!;

    }
}
