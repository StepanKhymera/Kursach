using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    public partial class Address
    {
        public Address()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            Offices = new HashSet<Office>();
        }

        [Key]
        [Column("address_id")]
        public int AddressId { get; set; }
        [Required]
        [Column("city")]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [Column("street")]
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [Column("house_no")]
        [StringLength(50)]
        public string HouseNo { get; set; }
        [Column("postal_code")]
        public int PostalCode { get; set; }

        [InverseProperty(nameof(Customer.Address))]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty(nameof(Employee.Address))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(Office.Address))]
        public virtual ICollection<Office> Offices { get; set; }
    }
}
