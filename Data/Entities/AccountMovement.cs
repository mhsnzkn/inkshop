using Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AccountMovement : Entity, IDatedEntity
    {
        public Office Office { get; set; }
        public int OfficeId { get; set; }
        public AccountEntity Entity { get; set; }
        public int EntityId { get; set; }
        public AccountType Type { get; set; }
        public int TypeId { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal Income { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Expense { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? UptDate { get; set; }
        public DateTime CrtDate { get; set; }
    }
}
