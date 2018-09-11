using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.ServiceRequest
{
    public class ServiceRequestPostViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int PartnerId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public int? VehicleTypeId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        public DateTime SubmittedDate { get; set; }

        [Required]
        public string ServiceRequestStatus { get; set; }

        [Required]
        public string PaymentStatus { get; set; }
    }
}
