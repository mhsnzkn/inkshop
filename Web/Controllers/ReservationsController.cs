using AutoMapper;
using Business.Abstract;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Dtos;
using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Accountant + "," + UserRoles.Supervisor)]
    public class ReservationsController : Controller
    {
        private readonly IOrderManager orderManager;
        private readonly IOrderTypeManager orderTypeManager;
        private readonly IOfficeManager officeManager;
        private readonly ICurrencyManager currencyManager;
        private readonly ICountryManager countryManager;
        private readonly IPersonnelManager personnelManager;
        private readonly IMapper mapper;

        public ReservationsController(IOrderManager orderManager, IOrderTypeManager orderTypeManager,
            IOfficeManager officeManager, ICurrencyManager currencyManager, ICountryManager countryManager,
            IPersonnelManager personnelManager, IMapper mapper)
        {
            this.orderManager = orderManager;
            this.orderTypeManager = orderTypeManager;
            this.officeManager = officeManager;
            this.currencyManager = currencyManager;
            this.countryManager = countryManager;
            this.personnelManager = personnelManager;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = new ReservationViewModel()
            {
                OrderType = await orderTypeManager.GetForDropDown(),
                Office = await officeManager.GetForDropDown(),
                Country = await countryManager.GetForDropDown(),
                Currency = await currencyManager.GetForDropDown(),
                ArtistPersonnel = await personnelManager.GetArtistPersonnelForDropDown(),
                InfoPersonnel = await personnelManager.GetInfoPersonnelForDropDown(),
                HanutPersonnel = await personnelManager.GetMiddlePersonnelForDropDown(),
            };
            if(id != 0)
            {
                var order = await orderManager.GetReservationByIdAsync(id);
                model.Reservation = mapper.Map<ReservationModel>(order);
            }else
            {
                model.Reservation = new ReservationModel();
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ReservationViewModel vModel)
        {
            if(vModel.Reservation.Id == 0)
                return BadRequest();

            var result = await orderManager.UpdateReservation(vModel.Reservation);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Approve([FromBody] OrderPostDto order)
        {
            if (order == null || order.Id == 0)
                return BadRequest();

            return Ok(await orderManager.ApproveReservation(order.Id));
        }
        [HttpPost]
        public async Task<IActionResult> Pay([FromBody] OrderPostDto order)
        {
            if (order == null || order.Id == 0)
                return BadRequest();

            return Ok(await orderManager.PayReservation(order.Id));
        }
        [HttpPost]
        public async Task<IActionResult> Cancel([FromBody] OrderPostDto dto)
        {
            if (dto == null || dto.Id == 0)
                return BadRequest();

            return Ok(await orderManager.CancelReservation(dto.Id,dto.Message ));
        }

        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            DataTableResult result = new DataTableResult();
            try
            {
                result = await orderManager.GetReservationDataTable(param);
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            return Ok(result);
        }
    }
}
