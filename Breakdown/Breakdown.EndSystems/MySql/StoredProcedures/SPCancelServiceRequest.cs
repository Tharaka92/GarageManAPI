using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPCancelServiceRequest : SP
    {
        public int ServiceRequestId { get; set; }

        public string ServiceRequestStatus { get; set; }
    }
}
