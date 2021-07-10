using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class UserTableDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? PersonnelId { get; set; }
        public string PersonnelName { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
