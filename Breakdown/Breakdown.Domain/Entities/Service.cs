using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<Package> Packages { get; set; }
    }
}
