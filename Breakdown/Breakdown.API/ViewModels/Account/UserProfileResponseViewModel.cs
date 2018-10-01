using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Account
{
    public class UserProfileResponseViewModel
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string VehicleNumber { get; set; }

        public string ProfileImageUrl { get; set; }

        public double AverageRating { get; set; }
    }
}
