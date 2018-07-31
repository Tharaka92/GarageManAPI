using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels
{
    public class ServiceViewModel
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        [StringLength(5)]
        public string UniqueCode { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<PackageViewModel> Packages { get; set; }
    }
}
