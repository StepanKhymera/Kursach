using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    [Table("Policy_InsuranceObject")]
    public partial class PolicyInsuranceObject
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("policy_id")]
        public int PolicyId { get; set; }
        [Column("parameter_id")]
        public int ParameterId { get; set; }
        [Required]
        [Column("paramenter_value")]
        [StringLength(50)]
        public string ParamenterValue { get; set; }

        [ForeignKey(nameof(ParameterId))]
        [InverseProperty(nameof(PropertyParameter.PolicyInsuranceObjects))]
        public virtual PropertyParameter Parameter { get; set; }
        [ForeignKey(nameof(PolicyId))]
        [InverseProperty("PolicyInsuranceObjects")]
        public virtual Policy Policy { get; set; }
    }
}
