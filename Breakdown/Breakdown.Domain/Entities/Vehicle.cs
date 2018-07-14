using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string YOM { get; set; }
        public bool IsDeleted { get; set; }
    }
}
