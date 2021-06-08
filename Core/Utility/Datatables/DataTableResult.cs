using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Datatables
{
    public class DataTableResult
    {
        public int Draw { get; set; }
        public uint RecordsFiltered { get; set; }
        public uint RecordsTotal { get; set; }
        public object Data { get; set; }
    }
}
