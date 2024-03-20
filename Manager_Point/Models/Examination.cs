using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class Examination
    {
        public int Id { get; set; }
        public int GradePointId { get; set; }
        public float Point { get; set; }
        public TypePoint TypePoint { get; set; }
    }
}
