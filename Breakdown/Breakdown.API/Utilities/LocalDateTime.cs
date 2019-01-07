using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.Utilities
{
    public static class LocalDateTime
    {
        public static DateTime Now()
        {
            TimeZoneInfo UAETimeZone = TimeZoneInfo.FindSystemTimeZoneById("Sri Lanka Standard Time");
            DateTime utc = DateTime.UtcNow;
            DateTime dateNow = TimeZoneInfo.ConvertTimeFromUtc(utc, UAETimeZone);
            return dateNow;
        }
    }
}
