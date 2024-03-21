namespace Manager_Point.Models
{
    public class Subject_Teacher
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int UserId { get; set; }

        public virtual Subject Subject { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
