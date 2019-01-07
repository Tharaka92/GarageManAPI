using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.ViewModels.Account
{
    public class LoginResponseViewModel
    {
        public int UserId { get; set; }

        public int? ServiceId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }

        public string FirebaseToken { get; set; }

        public string RoleName { get; set; }

        public string ServiceName { get; set; }

        public string UniqueCode { get; set; }

        public string VehicleNumber { get; set; }

        public string ProfileImageUrl { get; set; }

        public double AverageRating { get; set; }

        public bool IsApproved { get; set; }

        public bool IsBlocked { get; set; }

        public bool HasAPaymentMethod { get; set; }

        public bool HasADuePayment { get; set; }
    }
}
