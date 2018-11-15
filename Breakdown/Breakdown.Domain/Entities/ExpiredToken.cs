using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Breakdown.Domain.Entities
{
    public class ExpiredToken
    {
        [Key]
        public int ExpiredTokenId { get; set; }

        [StringLength(1500)]
        public string Token { get; set; }

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }

        public DateTime ExpiredDate { get; set; }

        public ApplicationUser User { get; set; }
    }
}
