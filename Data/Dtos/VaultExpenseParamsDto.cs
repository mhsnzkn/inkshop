using Core.Utility.Datatables;
using Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class VaultExpenseParamsDto : DataTableParams
    {
        public int OrderTypeId { get; set; }
        public PersonnelCategories PersonnelCategory { get; set; }
        public int PersonnelId { get; set; }

    }
}
