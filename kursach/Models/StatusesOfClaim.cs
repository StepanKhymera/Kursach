using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace kursach.Models
{
    [Table("Statuses_Of_Claims")]
    public partial class StatusesOfClaim
    {
        public StatusesOfClaim()
        {
            Claims = new HashSet<Claim>();
        }

        [Key]
        [Column("status_code")]
        public int StatusCode { get; set; }
        [Required]
        [Column("status_description")]
        [StringLength(100)]
        public string StatusDescription { get; set; }

        [InverseProperty(nameof(Claim.StatusCodeNavigation))]
        public virtual ICollection<Claim> Claims { get; set; }
    }
}
