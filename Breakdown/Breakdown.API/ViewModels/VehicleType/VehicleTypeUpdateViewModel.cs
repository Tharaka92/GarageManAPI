using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.VehicleType
{
    public class VehicleTypeUpdateViewModel : VehicleTypeBaseViewModel
    {
        [Required]
        public int VehicleTypeId { get; set; }
    }
}
