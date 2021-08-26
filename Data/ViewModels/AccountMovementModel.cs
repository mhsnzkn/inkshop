using Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Constants;

namespace Data.ViewModels
{
    public class AccountMovementModel
    {
        public int Id { get; set; }
        [Display(Name = "Şube")]
        [Required(ErrorMessage = "{0}" + UserMessages.Required)]
        public int OfficeId { get; set; }
        [Display(Name = "Cari")]
        [Required(ErrorMessage = "{0}" + UserMessages.Required)]
        public int EntityId { get; set; }
        [Display(Name = "Kalem")]
        [Required(ErrorMessage = "{0}" + UserMessages.Required)]
        public int TypeId { get; set; }
        [Display(Name = "Gelir")]
        public decimal Income { get; set; }
        [Display(Name = "Gider")]
        public decimal Expense { get; set; }
        [Display(Name = "Kur")]
        [Required(ErrorMessage = "{0}" + UserMessages.Required)]
        public int CurrencyId { get; set; }
        [Display(Name = "Açıklama")]
        [StringLength(500)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Tarih")]
        [Required(ErrorMessage = "{0}"+UserMessages.Required)]
        public DateTime Date { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [Display(Name = "Vade Tarihi")]
        [Required(ErrorMessage = "{0}" + UserMessages.Required)]
        public DateTime DueDate { get; set; } = DateTime.Now;
    }
}
