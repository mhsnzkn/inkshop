using Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class OrderPersonnel : Entity
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Personnel Personnel { get; set; }
        public int PersonnelId { get; set; }

    }
}
