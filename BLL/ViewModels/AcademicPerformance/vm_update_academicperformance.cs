using Data.Models.Enum;
using Manager_Point.Models.Enum;

namespace BLL.ViewModels.AcademicPerformance
{
    public class vm_update_academicperformance
    {
        public int GradePointId { get; set; }
        public Performance Performance { get; set; }
        public Status Status { get; set; }
    }
}
