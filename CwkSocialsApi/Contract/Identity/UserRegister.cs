﻿using System.ComponentModel.DataAnnotations;

namespace CwkSocialsApi.Contract.Identity
{
    public class UserRegister
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }
        public string CurrentCity { get; set; }
    }
}
