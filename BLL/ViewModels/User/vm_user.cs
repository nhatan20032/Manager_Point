using Manager_Point.Models.Enum;
using System.Text.Json.Serialization;

namespace BLL.ViewModels.User
{
    public class vm_user
    {
        public int Id { get; set; }
        public List<string>? Role_User { get; set; }
        public List<string>? Role_Code { get; set; }
        public List<int>? Role_id { get; set; }
        public List<string>? Subject_User { get; set; }
        public List<int>? Subject_id { get; set; }
        public List<string>? Teacher_Class { get; set; }
        public List<string>? Student_Class { get; set; }
        public List<int>? Student_Class_id { get; set; }
        public List<int>? Teacher_Class_id { get; set; }
        public List<int>? Course_id{ get; set; }
        public List<string>? Course_Name{ get; set; }
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
