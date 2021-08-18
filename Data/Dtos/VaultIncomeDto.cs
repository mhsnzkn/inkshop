using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class VaultIncomeDto
    {
        public int? Id { get; set; }
        public int OfficeId { get; set; }
        public int CurrencyId { get; set; }
        public string OfficeName { get; set; }
        public string CurrencyName { get; set; }
        public string CustomerFullName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsCreditCard { get; set; }
        public bool IsPaymentDone { get; set; }

    }
}
