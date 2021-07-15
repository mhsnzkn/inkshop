using Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class TransferTableDto
    {
        public int Id { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int CustomerCountryId { get; set; }
        public string CustomerCountryName { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public DateTime CrtDate { get; set; }
        public string Description { get; set; }
        public OrderStatus Status { get; set; }
        public bool? IsApproved { get; set; }
        public bool IsPaymentDone { get; set; }
    }
}
