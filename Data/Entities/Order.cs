using Data.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Order : Entity
    {
        public Office Office { get; set; }
        public int OfficeId { get; set; }
        public OrderType OrderType { get; set; }
        public int OrderTypeId { get; set; }
        public ApplicationUser Personnel { get; set; }
        public int PersonnelId { get; set; }
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(100)]
        public string CustomerSurname { get; set; }
        [StringLength(100)]
        public string CustomerCountry { get; set; }
        [StringLength(250)]
        public string CustomerHotel { get; set; }
        [StringLength(100)]
        public string CustomerRoomNumber { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Deposit { get; set; }
        [StringLength(50)]
        public string Currency { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        public DateTime Date { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsOrderApproved { get; set; }
        public bool IsReservationApproved { get; set; }
        public bool IsPaymentDone { get; set; }
        public bool IsCreditCard { get; set; }


    }
}
