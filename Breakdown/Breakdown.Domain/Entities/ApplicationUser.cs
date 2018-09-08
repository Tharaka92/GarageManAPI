using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Breakdown.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        [ForeignKey("Service")]
        public int? ServiceId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string VehicleNumber { get; set; }

        [StringLength(1000)]
        public string ProfileImageUrl { get; set; }

        public Service Service { get; set; }

        [InverseProperty("Customer")]
        public List<ServiceRequest> CustomerRequests { get; set; }

        [InverseProperty("Partner")]
        public List<ServiceRequest> PartnerJobs { get; set; }
    }
}
