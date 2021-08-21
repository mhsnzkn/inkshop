using Core.Utility.Datatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class VaultIncomeParamsDto:DataTableParams
    {
        public int OrderTypeId { get; set; }
        public bool? IsPaymentDone { get; set; }

    }
}
