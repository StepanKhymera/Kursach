using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    public partial class Office
    {
        public Office()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Column("office_id")]
        public int OfficeId { get; set; }
        [Column("address_id")]
        public int AddressId { get; set; }
        [Column("rent_per_month", TypeName = "decimal(8, 5)")]
        public decimal RentPerMonth { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Offices")]
        public virtual Address Address { get; set; }
        [InverseProperty(nameof(Employee.Position))]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
