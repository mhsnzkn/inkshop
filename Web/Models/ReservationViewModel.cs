﻿using Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Dtos;

namespace Web.Models
{
    public class ReservationViewModel
    {
        public ReservationDto Reservation { get; set; }
        public List<SelectListItem> OrderType { get; set; }
        public List<SelectListItem> Office { get; set; }
        public List<SelectListItem> Country { get; set; }
        public List<SelectListItem> Currency { get; set; }
        public List<SelectListItem> ArtistPersonnel { get; set; }
        public List<SelectListItem> InfoPersonnel { get; set; }
        public List<SelectListItem> HanutPersonnel { get; set; }
    }
}
