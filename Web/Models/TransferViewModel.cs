using Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Dtos;

namespace Web.Models
{
    public class TransferViewModel
    {
        public TransferDto Transfer { get; set; }
        public List<SelectListItem> Country { get; set; }
    }
}
