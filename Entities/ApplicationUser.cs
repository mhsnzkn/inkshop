using Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string job { get; set; }
        public Office Office { get; set; }
        public int OfficeId { get; set; }
        [Column(TypeName ="decimal(5,2)")]
        public decimal Commission { get; set; }

    }
}
