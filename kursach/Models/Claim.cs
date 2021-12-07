using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    public partial class Claim
    {
        public Claim()
        {
            ClaimPayments = new HashSet<ClaimPayment>();
        }

        [Key]
        [Column("claim_id")]
        public int ClaimId { get; set; }
        [Column("status_code")]
        public int StatusCode { get; set; }
        [Column("policy_id")]
        public int PolicyId { get; set; }
        [Column("date_of_claim", TypeName = "date")]
        public DateTime DateOfClaim { get; set; }
        [Column("amout_of_claim")]
        public int AmoutOfClaim { get; set; }

        [ForeignKey(nameof(PolicyId))]
        [InverseProperty("Claims")]
        public virtual Policy Policy { get; set; }
        [ForeignKey(nameof(StatusCode))]
        [InverseProperty(nameof(StatusesOfClaim.Claims))]
        public virtual StatusesOfClaim StatusCodeNavigation { get; set; }
        [InverseProperty(nameof(ClaimPayment.Claim))]
        public virtual ICollection<ClaimPayment> ClaimPayments { get; set; }
    }
}
