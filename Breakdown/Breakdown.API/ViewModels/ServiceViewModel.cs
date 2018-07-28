using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels
{
    public class ServiceViewModel
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<PackageViewModel> Packages { get; set; }
    }
}
