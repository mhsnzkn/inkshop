using AutoMapper;
using Business.Abstract;
using Core.Utility.Datatables;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderManager orderManager;
        private readonly IOrderTypeManager orderTypeManager;
        private readonly IOfficeManager officeManager;
        private readonly ICurrencyManager currencyManager;
        private readonly ICountryManager countryManager;
        private readonly IMapper mapper;

        public OrdersController(IOrderManager orderManager, IOrderTypeManager orderTypeManager,
            IOfficeManager officeManager, ICurrencyManager currencyManager, ICountryManager countryManager,
            IMapper mapper)
        {
            this.orderManager = orderManager;
            this.orderTypeManager = orderTypeManager;
            this.officeManager = officeManager;
            this.currencyManager = currencyManager;
            this.countryManager = countryManager;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = new OrderViewModel()
            {
                OrderType = await orderTypeManager.GetForDropDown(),
                Office = await officeManager.GetForDropDown(),
                Country = await countryManager.GetForDropDown(),
                Currency = await currencyManager.GetForDropDown(),
            };
            model.Order = id == 0 ? new OrderAddDto() : mapper.Map<OrderAddDto>(await orderManager.GetByIdAsync(id));

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderViewModel vModel)
        {
            Core.Utility.Result result = null;
            if(vModel.Order.Id == 0)
            {
                result = await orderManager.AddOrder(vModel.Order);
            }
            else
            {
                result = await orderManager.UpdateOrder(vModel.Order);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Approve([FromBody] OrderPostDto order)
        {
            if (order == null || order.Id == 0)
                return BadRequest();

            return Ok(await orderManager.ApproveOrder(order.Id));
        }
        [HttpPost]
        public async Task<IActionResult> Cancel([FromBody] OrderPostDto dto)
        {
            if (dto == null || dto.Id == 0)
                return BadRequest();

            return Ok(await orderManager.CancelOrder(dto.Id,dto.Message ));
        }

        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await orderManager.GetOrderDataTable(param));
        }
    }
}
