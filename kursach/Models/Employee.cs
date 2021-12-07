using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Policies = new HashSet<Policy>();
        }

        [Key]
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("office_id")]
        [StringLength(200)]
        public string OfficeId { get; set; }
        [Column("bithday", TypeName = "date")]
        public DateTime? Bithday { get; set; }
        [Column("address_id")]
        public int? AddressId { get; set; }
        [Column("phone_number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [Column("position_id")]
        public int? PositionId { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Employees")]
        public virtual Address Address { get; set; }
        [ForeignKey(nameof(PositionId))]
        [InverseProperty(nameof(Office.Employees))]
        public virtual Office Position { get; set; }
        [ForeignKey(nameof(PositionId))]
        [InverseProperty(nameof(PositionType.Employees))]
        public virtual PositionType PositionNavigation { get; set; }
        [InverseProperty(nameof(Policy.Employee))]
        public virtual ICollection<Policy> Policies { get; set; }
    }
}
