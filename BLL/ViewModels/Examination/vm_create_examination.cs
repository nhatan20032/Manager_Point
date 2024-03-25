using Manager_Point.Models.Enum;

namespace BLL.ViewModels.Examination
{
    public class vm_create_examination
    {
        public int GradePointId { get; set; }
        public float Point { get; set; }
        public TypePoint TypePoint { get; set; }
    }
}
