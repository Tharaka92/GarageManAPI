using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPInsertService : SP
    {
        public string ServiceName { get; set; }
        public string UniqueCode { get; set; }
    }
}
