﻿using Data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class OrderType : Entity
    {
        [StringLength(100)]
        public string Name { get; set; }

    }
}
