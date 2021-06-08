using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
