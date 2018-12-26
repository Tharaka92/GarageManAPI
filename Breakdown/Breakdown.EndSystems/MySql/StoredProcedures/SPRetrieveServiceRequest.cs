using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPRetrieveServiceRequest : SP
    {
        public int? PartnerId { get; set; }
        public int? CustomerId { get; set; }
        public int? ServiceRequestId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
