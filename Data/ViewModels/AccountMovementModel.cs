using Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class AccountMovementModel
    {
        public int OfficeId { get; set; }
        public int EntityId { get; set; }
        public int TypeId { get; set; }

        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public int CurrencyId { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
    }
}
