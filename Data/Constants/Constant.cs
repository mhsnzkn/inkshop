using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Constants
{
    public enum OrderTypeId
    {
        Dovme = 1,
        MakePiercing = 2
    }

    public struct OrderTypeString
    {
        public const string CoverUp = "coverup";
        public const string Freehand = "freehand";
        public const string Refresh = "refresh";
        public const string TouchUp = "touchup";
    }

    public enum OrderStatus
    {
        Transfer,
        Order,
        Reservation
    }

    public struct UserRoles
    {
        public const string Admin = "admin";
        public const string Accountant = "accountant";
        public const string Supervisor = "supervisor";
        public const string Info = "info";
    }

    public enum OrderPersonnelJob
    {
        Artist,
        Info,
        Hanut
    }

    public enum PersonnelCategories
    {
        Franchising = 1,
        [Display(Name = "Dış İnfo")]
        DisInfo,
        [Display(Name = "Hanutçu")]
        Hanutcu,
        [Display(Name = "İç İnfo")]
        IcInfo,
        Artist,
    }
}
