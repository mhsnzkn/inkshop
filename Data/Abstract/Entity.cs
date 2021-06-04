using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Abstract
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public DateTime CrtDate { get; set; }
        public DateTime? UptDate { get; set; }
    }
}
