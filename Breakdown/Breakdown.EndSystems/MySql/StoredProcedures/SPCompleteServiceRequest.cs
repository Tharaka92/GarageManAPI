using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPCompleteServiceRequest : SP
    {
        public int ServiceRequestId { get; set; }

        public string ServiceRequestStatus { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
