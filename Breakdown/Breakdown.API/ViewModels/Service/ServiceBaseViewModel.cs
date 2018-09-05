using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Service
{
    public class ServiceBaseViewModel
    {
        [Required]
        public string ServiceName { get; set; }

        [StringLength(5)]
        public string UniqueCode { get; set; }
    }
}
