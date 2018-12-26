using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Domain.DTOs
{
    public class ServiceRequestDto
    {
        public int ServiceRequestId { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string ServiceRequestStatus { get; set; }
        public decimal PackagePrice { get; set; }
        public decimal PartnerAmount { get; set; }
        public string PaymentType { get; set; }
        public string PartnerName { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public string VehicleTypeName { get; set; }
        public string PackageName { get; set; }
    }
}
