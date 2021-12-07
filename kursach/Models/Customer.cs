using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Policies = new HashSet<Policy>();
        }

        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("date_of_birth", TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Column("gender")]
        [StringLength(20)]
        public string Gender { get; set; }
        [Column("year_income")]
        public int YearIncome { get; set; }
        [Column("address_id")]
        public int AddressId { get; set; }
        [Column("phone_number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(AddressId))]

        [InverseProperty("Customers")]
        public virtual Address Address { get; set; }

        [InverseProperty(nameof(Policy.Customer))]
        public virtual ICollection<Policy> Policies { get; set; }
    }
}
