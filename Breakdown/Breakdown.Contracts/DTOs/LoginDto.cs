using System.ComponentModel.DataAnnotations;

namespace Breakdown.Contracts.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }

        public string RoleName { get; set; }
    }
}
