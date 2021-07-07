﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Constants
{
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
        public const string Admin = "Admin";
        public const string Accountant = "Accountant";
        public const string Supervisor = "Supervisor";
        public const string Info = "Info";
    }
}
