using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels
{
    public class PackageViewModel
    {
        public int PackageId { get; set; }
        public int? ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }

        public ServiceViewModel Service { get; set; }
    }
}
