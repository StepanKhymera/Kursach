using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    [Index(nameof(CustomerId), Name = "IX_test")]
    public partial class Policy
    {
        public Policy()
        {
            Claims = new HashSet<Claim>();
            PolicyInsuranceObjects = new HashSet<PolicyInsuranceObject>();
            PolicyPayments = new HashSet<PolicyPayment>();
        }

        [Key]
        [Column("policy_id")]
        public int PolicyId { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        [Column("insurance_type")]
        public int InsuranceType { get; set; }
        [Column("policy_start_date", TypeName = "date")]
        public DateTime PolicyStartDate { get; set; }
        [Column("policy_expiration_date", TypeName = "date")]
        public DateTime? PolicyExpirationDate { get; set; }
        [Column("anual_fee")]
        public int AnualFee { get; set; }
        [Column("coverage")]
        public int Coverage { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Policies")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Policies")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(InsuranceType))]
        [InverseProperty(nameof(Types_Of_Insurance.Policies))]
        public virtual Types_Of_Insurance InsuranceTypeNavigation { get; set; }
        [InverseProperty(nameof(Claim.Policy))]
        public virtual ICollection<Claim> Claims { get; set; }
        [InverseProperty(nameof(PolicyInsuranceObject.Policy))]
        public virtual ICollection<PolicyInsuranceObject> PolicyInsuranceObjects { get; set; }
        [InverseProperty(nameof(PolicyPayment.Policy))]
        public virtual ICollection<PolicyPayment> PolicyPayments { get; set; }
    }
}
