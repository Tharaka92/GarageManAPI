using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.VehicleTypes
{
    public class VehicleTypeGetViewModel
    {
        public int VehicleTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
