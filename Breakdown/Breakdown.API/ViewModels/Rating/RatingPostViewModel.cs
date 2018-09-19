using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Rating
{
    public class RatingPostViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ServiceRequestId { get; set; }

        public double RatingValue { get; set; }

        public string Comment { get; set; }
    }
}
