using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    [Table("Policy_Payments")]
    public partial class PolicyPayment
    {
        [Key]
        [Column("payment_id")]
        public int PaymentId { get; set; }
        [Column("policy_id")]
        public int PolicyId { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("sum")]
        public int Sum { get; set; }

        [ForeignKey(nameof(PolicyId))]
        [InverseProperty("PolicyPayments")]
        public virtual Policy Policy { get; set; }
    }
}
