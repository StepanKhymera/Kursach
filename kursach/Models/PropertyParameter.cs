using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    [Table("Property_Parameters")]
    public partial class PropertyParameter
    {
        public PropertyParameter()
        {
            PolicyInsuranceObjects = new HashSet<PolicyInsuranceObject>();
        }

        [Key]
        [Column("paramater_id")]
        public int ParamaterId { get; set; }
        [Required]
        [Column("parameter_description")]
        [StringLength(50)]
        public string ParameterDescription { get; set; }
        [Column("type_of_insurance")]
        public int TypeOfInsurance { get; set; }

        [ForeignKey(nameof(TypeOfInsurance))]
        [InverseProperty(nameof(Types_Of_Insurance.PropertyParameters))]
        public virtual Types_Of_Insurance TypeOfInsuranceNavigation { get; set; }
        [InverseProperty(nameof(PolicyInsuranceObject.Parameter))]
        public virtual ICollection<PolicyInsuranceObject> PolicyInsuranceObjects { get; set; }
    }
}
