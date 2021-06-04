using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public DateTime CrtDate { get; set; }
        public DateTime? UptDate { get; set; }
    }
}
