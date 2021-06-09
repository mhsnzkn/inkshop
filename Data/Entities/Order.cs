using Core.Data;
using System;
using System.Collections.Generic;
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
        public int OrderTypeId { get; set; }
        public ApplicationUser Personnel { get; set; }
        public int? PersonnelId { get; set; }
        public Country CustomerCountry { get; set; }
        public int CustomerCountryId { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(100)]
        public string CustomerSurname { get; set; }
        [StringLength(500)]
        public string CustomerAdress { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Deposit { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        public DateTime Date { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsOrderApproved { get; set; }
        public bool IsReservationApproved { get; set; }
        public bool IsPaymentDone { get; set; }
        public bool IsCreditCard { get; set; }
        public DateTime? UptDate { get; set; }
        public DateTime CrtDate { get; set; }


    }
}
