﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPRetrieveVehicle : SP
    {
        public int? VehicleId { get; set; }
        public string UserId { get; set; }
    }
}
