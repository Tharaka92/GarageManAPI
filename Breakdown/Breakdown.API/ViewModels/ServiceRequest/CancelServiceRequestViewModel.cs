using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.ServiceRequest
{
    public class CancelServiceRequestViewModel
    {
        [Required]
        public int ServiceRequestId { get; set; }

        [Required]
        public string ServiceRequestStatus { get; set; }
    }
}
