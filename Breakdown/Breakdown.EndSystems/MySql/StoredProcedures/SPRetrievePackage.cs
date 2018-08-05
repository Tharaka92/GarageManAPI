using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPRetrievePackage : SP
    {
        public int? PackageId { get; set; }
        public int? ServiceId { get; set; }
    }
}
