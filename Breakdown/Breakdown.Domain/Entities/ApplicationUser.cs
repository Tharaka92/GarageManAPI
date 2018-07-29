using Microsoft.AspNetCore.Identity;

namespace Breakdown.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? ServiceId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public Service Service { get; set; }
    }
}
