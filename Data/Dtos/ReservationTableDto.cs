﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class ReservationTableDto
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int OrderTypeId { get; set; }
        public string OrderTypeName { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string PersonnelName { get; set; }
        public int CustomerCountryId { get; set; }
        public string CustomerCountryName { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerAdress { get; set; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public bool TypeCoverUp { get; set; }
        public bool TypeRefresh { get; set; }
        public bool TypeFreeHand { get; set; }
        public bool TypeTouchUp { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsCreditCard { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsReservationApproved { get; set; }
        public bool IsPaymentDone { get; set; }

    }
}
