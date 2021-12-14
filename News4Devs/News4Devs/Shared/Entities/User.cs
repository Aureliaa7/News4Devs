using System;
using System.Collections.Generic;

namespace News4Devs.Shared.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string ProfilePhotoName { get; set; }
        
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }


        // the password in plaintext; it won't be mapped to DB
        public string Password { get; set; }

        public virtual ICollection<ChatMessage> MessagesFromUsers { get; set; }

        public virtual ICollection<ChatMessage> MessagesToUsers { get; set; }

        public User()
        {
            MessagesFromUsers = new HashSet<ChatMessage>();
            MessagesToUsers = new HashSet<ChatMessage>();
        }
    }
}
