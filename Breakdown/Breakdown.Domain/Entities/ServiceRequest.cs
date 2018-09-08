using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int CustomerId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int PartnerId { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }

        [ForeignKey("VehicleType")]
        public int? VehicleTypeId { get; set; }

        [ForeignKey("Package")]
        public int PackageId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }
        
        public ApplicationUser Customer { get; set; }

        public ApplicationUser Partner { get; set; }

        public Service Service { get; set; }

        public VehicleType VehicleType { get; set; }

        public Package Package { get; set; }
    }
}
