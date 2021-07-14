using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        [Required(ErrorMessage ="Sipariş türü boş bırakılamaz")]
        public int OrderTypeId { get; set; }
        [Required(ErrorMessage = "Kur boş bırakılamaz")]
        public int CurrencyId { get; set; }
        [Required(ErrorMessage = "Ülke boş bırakılamaz")]
        public int CustomerCountryId { get; set; }
        public int? PersonnelId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerHotel { get; set; }
        public string CustomerRoomNumber { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public bool TypeCoverUp { get; set; }
        public bool TypeRefresh { get; set; }
        public bool TypeFreeHand { get; set; }
        public bool TypeTouchUp { get; set; }
        public bool IsTransfer { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public bool IsCreditCard { get; set; }
    }
}
