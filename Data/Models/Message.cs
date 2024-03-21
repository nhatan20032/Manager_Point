using Manager_Point.Models.Enum;

namespace Manager_Point.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string? Content { get; set; }
        public Status Status { get; set; }

        public virtual Class Class { get; set; } = null!;
    }
}
