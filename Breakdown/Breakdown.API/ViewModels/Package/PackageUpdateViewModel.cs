using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Package
{
    public class PackageUpdateViewModel : PackageBaseViewModel
    {
        [Required]
        public int PackageId { get; set; }
    }
}
