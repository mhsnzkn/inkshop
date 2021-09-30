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
    public class AccountMovementSumDto
    {
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public string CurrencyName { get; set; }
        public string VaultName { get; set; }
    }
}
