using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPInsertVehicleType : SP
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
