using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class Rating
    {
        public int RatingId { get; set; }

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }

        [ForeignKey("ServiceRequest")]
        public int ServiceRequestId { get; set; }

        public double RatingValue { get; set; }

        public string Comment { get; set; }

        public ApplicationUser User { get; set; }

        public ServiceRequest ServiceRequest { get; set; }
    }
}
