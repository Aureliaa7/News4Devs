using System.ComponentModel.DataAnnotations;

namespace News4Devs.Core.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Email { get; init; }
        
        [Required]
        public string Password { get; init; }
    }
}
