using Core.Data;
using Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class AccountTypeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad"+UserMessages.Required)]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

    }
}
