using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class VaultExpenseDto
    {
        public int OrderId { get; set; }
        public string OfficeName { get; set; }
        public string CurrencyName { get; set; }
        public string PersonnelFullName { get; set; }
        public string PersonnelJob { get; set; }
        public decimal Price { get; set; }
        public DateTime ApproveDate { get; set; }
        public bool IsCreditCard { get; set; }
        public bool IsPaymentDone { get; set; }

    }
}
