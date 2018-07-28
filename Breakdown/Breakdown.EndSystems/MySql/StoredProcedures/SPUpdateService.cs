using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPUpdateService : SP
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
    }
}
