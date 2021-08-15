using Core.Data;
using Data.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Dtos
{
    public class PersonnelTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string job { get; set; }
        public string CategoryName { get; set; }
        public int? OfficeId { get; set; }
        public decimal Commission { get; set; }
    }
}
