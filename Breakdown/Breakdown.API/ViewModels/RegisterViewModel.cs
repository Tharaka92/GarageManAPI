using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }

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
