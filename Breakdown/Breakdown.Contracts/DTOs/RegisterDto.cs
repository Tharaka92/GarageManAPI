﻿using System.ComponentModel.DataAnnotations;

namespace Breakdown.Contracts.DTOs
{
    public class RegisterDto
    {
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Token { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
