using Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class AccountEntityModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad"+UserMessages.Required)]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Mail { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }
}
