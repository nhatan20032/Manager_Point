using System.ComponentModel.DataAnnotations;

namespace BLL.Author
{
    public class AuthenticateRequest
    {
        [Required]
        public string? UserCode { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
