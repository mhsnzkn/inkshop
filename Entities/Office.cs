using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Office : Entity
    {
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

    }
}
