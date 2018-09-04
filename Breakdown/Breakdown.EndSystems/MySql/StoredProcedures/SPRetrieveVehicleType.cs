using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPRetrieveVehicleType : SP
    {
        public int? VehicleTypeId { get; set; }
    }
}
