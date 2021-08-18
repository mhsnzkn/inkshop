using Core.Data;
using Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Order : Entity, IDatedEntity
    {
        public Office Office { get; set; }
        public int OfficeId { get; set; }
        public OrderType OrderType { get; set; }
        public int? OrderTypeId { get; set; }
        public Currency Currency { get; set; }
        public int? CurrencyId { get; set; }
        public Country CustomerCountry { get; set; }
        public int CustomerCountryId { get; set; }
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(100)]
        public string CustomerSurname { get; set; }
        [StringLength(250)]
        public string CustomerHotel { get; set; }
        [StringLength(50)]
        public string CustomerRoomNumber { get; set; }
        [StringLength(50)]
        public string CustomerPhoneNumber { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Deposit { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        public DateTime Date { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(100)]
        public string CancellationReason { get; set; }
        public OrderStatus Status { get; set; }
        public bool? IsApproved { get; set; }
        public bool IsPaymentDone { get; set; }
        public bool IsCreditCard { get; set; }
        public bool IsTransfer { get; set; }
        public int PersonCount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public DateTime? UptDate { get; set; }
        public DateTime CrtDate { get; set; }

        public ICollection<OrderPersonnel> OrderPersonnel { get; set; }

    }
}
