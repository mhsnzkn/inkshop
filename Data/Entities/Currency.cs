using Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Currency : Entity
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(10)]
        public string ShortName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
