using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        [StringLength(5)]
        public string UniqueCode { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<Package> Packages { get; set; }
    }
}
