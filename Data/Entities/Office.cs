using Core.Data;
using Data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Office : DefinitionEntity, IDatedEntity
    {
        [StringLength(100)]
        public string City { get; set; }
        public DateTime? UptDate { get; set; }
        public DateTime CrtDate { get; set; }
    }
}
