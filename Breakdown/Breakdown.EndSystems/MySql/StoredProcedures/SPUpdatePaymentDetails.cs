﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Breakdown.EndSystems.MySql.StoredProcedures
{
    public class SPUpdatePaymentDetails : SP
    {
        public int ServiceRequestId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal PackagePrice { get; set; }

        public decimal PartnerAmount { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentType { get; set; }
    }
}
