using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IDatedEntity
    {
        public DateTime? UptDate { get; set; }
        public DateTime CrtDate { get; set; }
    }
}
