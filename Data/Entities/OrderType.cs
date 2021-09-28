using Core.Data;
using Data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class OrderType : DefinitionEntity, IDatedEntity
    {
        public DateTime? UptDate { get; set; }
        public DateTime CrtDate { get; set; }

    }
}
