using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    [Table("Position_Types")]
    public partial class PositionType
    {
        public PositionType()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Column("position_type_id")]
        public int PositionTypeId { get; set; }
        [Required]
        [Column("position_name")]
        [StringLength(50)]
        public string PositionName { get; set; }
        [Column("role")]
        public int Role { get; set; }

        [InverseProperty(nameof(Employee.PositionNavigation))]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
