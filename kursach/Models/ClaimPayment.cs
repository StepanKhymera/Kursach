using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    [Table("Claim_Payments")]
    public partial class ClaimPayment
    {
        [Key]
        [Column("claim_payment_id")]
        public int ClaimPaymentId { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("sum", TypeName = "decimal(6, 6)")]
        public decimal Sum { get; set; }
        [Column("claim_id")]
        public int ClaimId { get; set; }

        [ForeignKey(nameof(ClaimId))]
        [InverseProperty("ClaimPayments")]
        public virtual Claim Claim { get; set; }
    }
}
