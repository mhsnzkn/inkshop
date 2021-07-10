using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class TransferDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Şube boş bırakılamaz")]
        public int? OfficeId { get; set; }
        [Required(ErrorMessage = "Ülke boş bırakılamaz")]
        public int CustomerCountryId { get; set; }
        [Required(ErrorMessage = "Müşteri adı boş bırakılamaz")]
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        [Required(ErrorMessage = "Hotel adı boş bırakılamaz")]
        public string CustomerHotel { get; set; }
        public string CustomerRoomNumber { get; set; }
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
