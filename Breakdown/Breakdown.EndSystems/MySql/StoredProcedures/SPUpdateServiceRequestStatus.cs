using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPUpdateServiceRequestStatus : SP
    {
        public int ServiceRequestId { get; set; }

        public string ServiceRequestStatus { get; set; }
    }
}
