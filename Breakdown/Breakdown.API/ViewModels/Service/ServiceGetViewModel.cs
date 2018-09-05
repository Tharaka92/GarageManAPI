using Breakdown.API.ViewModels.Package;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Service
{
    public class ServiceGetViewModel : ServiceBaseViewModel
    {
        public int ServiceId { get; set; }
    }
}
