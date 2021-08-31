using System;
using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class TransferModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Şube boş bırakılamaz")]
        public int? OfficeId { get; set; }
        [Required(ErrorMessage ="Sipariş türü boş bırakılamaz")]
        public int OrderTypeId { get; set; }
        [Required(ErrorMessage = "Ülke boş bırakılamaz")]
        public int CustomerCountryId { get; set; }
        [Required(ErrorMessage = "Müşteri adı boş bırakılamaz")]
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        [Required(ErrorMessage = "Hotel adı boş bırakılamaz")]
        public string CustomerHotel { get; set; }
        public string CustomerRoomNumber { get; set; }
        public string CustomerPhoneNumber { get; set; }
        [Required(ErrorMessage = "Tarih boş bırakılamaz")]
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        [Required(ErrorMessage = "Kişi sayısı boş bırakılamaz")]
        public int PersonCount { get; set; } = 1;
    }
}
