using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPUpdateVehicleType : SP
    {
        public int VehicleTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
