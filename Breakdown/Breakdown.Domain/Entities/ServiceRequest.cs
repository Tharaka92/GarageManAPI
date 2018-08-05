using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }

        public int CustomerId { get; set; }

        public int PartnerId { get; set; }

        public int ServiceId { get; set; }

        public int PackageId { get; set; }

        public int VehicleId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
    }
}
