using System;

namespace News4Devs.Shared.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string ProfilePhotoName { get; set; }
    }
}
