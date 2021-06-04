using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public interface IEntity
    {
        public int Id { get; set; }
        public DateTime CrtDate { get; set; }
        public DateTime? UptDate { get; set; }
    }
}
