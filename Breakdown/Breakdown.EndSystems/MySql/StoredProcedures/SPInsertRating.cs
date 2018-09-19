using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPInsertRating : SP
    {
        public int UserId { get; set; }

        public int ServiceRequestId { get; set; }

        public double RatingValue { get; set; }

        public string Comment { get; set; }
    }
}
