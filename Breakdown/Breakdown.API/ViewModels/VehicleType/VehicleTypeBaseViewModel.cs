using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.VehicleType
{
    public class VehicleTypeBaseViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }
    }
}
