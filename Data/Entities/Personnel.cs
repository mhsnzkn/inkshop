using Core.Data;
using Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Personnel : Entity, IDatedEntity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string job { get; set; }
        public PersonnelCategories Category { get; set; }
        public Office Office { get; set; }
        public int? OfficeId { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Commission { get; set; }
        public DateTime? UptDate { get; set; }
        public DateTime CrtDate { get; set; }
    }
}
