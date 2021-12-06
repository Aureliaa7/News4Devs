using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace News4Devs.Shared.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public byte[] ProfilePhotoContent { get; set; }
    }
}
