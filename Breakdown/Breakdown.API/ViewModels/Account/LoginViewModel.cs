using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Account
{
    public class LoginViewModel
    {
        public int UserId { get; set; }

        public int? ServiceId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }

        public string RoleName { get; set; }

        public string ServiceName { get; set; }

        public string UniqueCode { get; set; }
    }
}
