using Core.Data;
using Data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AccountEntity : DefinitionEntity, IDatedEntity
    {
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Mail { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        public DateTime? UptDate { get; set; }
        public DateTime CrtDate { get; set; }
    }
}
