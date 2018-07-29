using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels
{
    public class VehicleViewModel
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
