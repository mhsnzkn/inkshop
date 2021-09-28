using Core.Utility.Datatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class AccountParamsDto : DataTableParams
    {
        public int OfficeId { get; set; }
        public int EntityId { get; set; }
        public int TypeId { get; set; }
        public int VaultId { get; set; }
        //public int VaultOutId { get; set; }

    }
}
