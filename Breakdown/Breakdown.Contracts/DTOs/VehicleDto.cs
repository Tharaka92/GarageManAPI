using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Contracts.DTOs
{
    public class VehicleDto
    {
        public int VehicleId { get; set; }
        public string UserId { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string YOM { get; set; }
        public bool IsDeleted { get; set; }
    }
}
