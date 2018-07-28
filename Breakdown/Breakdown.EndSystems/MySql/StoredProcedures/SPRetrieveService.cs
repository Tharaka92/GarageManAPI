using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPRetrieveService : SP
    {
        public int? ServiceId { get; set; }
    }
}
