using Manager_Point.Models.Enum;
using System.Text.Json.Serialization;

namespace BLL.ViewModels.User
{
    public class vm_user
    {
        public int Id { get; set; }
        public ICollection<string>? Role_User { get; set; }
        public ICollection<string>? Subject_User { get; set; }
        public ICollection<string>? User_Class { get; set; }
        public string? User_Code { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Address { get; set; }
        public string? Nation { get; set; }
        public Status Status { get; set; }
    }
}
