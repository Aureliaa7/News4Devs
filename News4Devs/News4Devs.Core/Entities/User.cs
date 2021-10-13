using System;

namespace News4Devs.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string ProfilePhotoPath { get; set; }
        
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }


        // the password in plaintext; it won't be mapped to DB
        public string Password { get; set; }
    }
}
