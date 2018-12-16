using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPRetrievePartnerPayment : SP
    {
        public int PartnerId { get; set; }
    }
}
