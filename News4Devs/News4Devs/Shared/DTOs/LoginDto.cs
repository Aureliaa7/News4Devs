﻿using System.ComponentModel.DataAnnotations;

namespace News4Devs.Core.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}