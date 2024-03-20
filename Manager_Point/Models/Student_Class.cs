namespace Manager_Point.Models
{
    public class Student_Class
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
