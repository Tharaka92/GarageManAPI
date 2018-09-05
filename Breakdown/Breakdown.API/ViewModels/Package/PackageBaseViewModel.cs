using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Package
{
    public class PackageBaseViewModel
    {
        public int? ServiceId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }
    }
}
