using Microsoft.AspNetCore.Identity;

namespace Breakdown.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
