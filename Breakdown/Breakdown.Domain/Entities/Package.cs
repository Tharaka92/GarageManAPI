using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class Package
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
