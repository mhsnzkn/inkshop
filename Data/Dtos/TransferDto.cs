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
        [Required(ErrorMessage = "Ülke boş bırakılamaz")]
        public int CustomerCountryId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerHotel { get; set; }
        public string CustomerRoomNumber { get; set; }
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
