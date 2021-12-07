using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    [Table("Types_Of_Insurance")]
    public partial class Types_Of_Insurance
    {
        public Types_Of_Insurance()
        {
            Policies = new HashSet<Policy>();
            PropertyParameters = new HashSet<PropertyParameter>();
        }

        [Key]
        [Column("insurance_type_code")]
        public int InsuranceTypeCode { get; set; }
        [Required]
        [Column("type_description")]
        [StringLength(200)]
        public string TypeDescription { get; set; }
        [Column("average_price_per_year")]
        public int AveragePricePerYear { get; set; }
        [Column("average_risk", TypeName = "decimal(6, 3)")]
        public decimal AverageRisk { get; set; }
        [Column("average_coverage")]
        public int AverageCoverage { get; set; }

        [InverseProperty(nameof(Policy.InsuranceTypeNavigation))]
        public virtual ICollection<Policy> Policies { get; set; }
        [InverseProperty(nameof(PropertyParameter.TypeOfInsuranceNavigation))]
        public virtual ICollection<PropertyParameter> PropertyParameters { get; set; }
    }
}
