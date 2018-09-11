using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPInsertServiceRequest : SP
    {
        public int CustomerId { get; set; }

        public int PartnerId { get; set; }

        public int ServiceId { get; set; }

        public int? VehicleTypeId { get; set; }

        public int PackageId { get; set; }

        public DateTime SubmittedDate { get; set; }

        public string ServiceRequestStatus { get; set; }

        public string PaymentStatus { get; set; }
    }
}
