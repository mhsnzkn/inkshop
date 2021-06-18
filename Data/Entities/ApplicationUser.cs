using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Personnel Personnel { get; set; }
        public int? PersonnelId { get; set; }

    }
}
