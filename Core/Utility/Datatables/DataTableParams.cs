using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility.Datatables
{
    public class DataTableParams
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<DtOrder> order { get; set; }
        public List<DtColumns> columns { get; set; }
        public DtSearch search { get; set; }
        public DateTime? minDate { get; set; }
        public DateTime? maxDate { get; set; }

    }

    public class DtColumns
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public DtSearch search { get; set; }
    }

    public class DtSearch
    {
        public bool regex { get; set; }
        public string value { get; set; }
    }

    public class DtOrder
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}
