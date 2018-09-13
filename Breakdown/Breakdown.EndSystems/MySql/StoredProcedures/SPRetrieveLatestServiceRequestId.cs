using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPRetrieveLatestServiceRequestId : SP
    {
        public int PartnerId { get; set; }

        public int CustomerId { get; set; }

        public string ServiceRequestStatus { get; set; }
    }
}
