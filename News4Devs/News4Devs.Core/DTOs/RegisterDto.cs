using System.ComponentModel.DataAnnotations;

namespace News4Devs.Core.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; init; }

        [Required]
        public string LastName { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        public string ProfilePhotoPath { get; init; }
    }
}
