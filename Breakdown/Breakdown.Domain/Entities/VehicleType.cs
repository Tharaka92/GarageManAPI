using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class VehicleType
    {
        [Key]
        public int VehicleTypeId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
