using Business.Abstract;
using Core.Utility.Datatables;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderManager orderManager;
        private readonly IOrderTypeManager orderTypeManager;
        private readonly IOfficeManager officeManager;

        public OrdersController(IOrderManager orderManager, IOrderTypeManager orderTypeManager,
            IOfficeManager officeManager)
        {
            this.orderManager = orderManager;
            this.orderTypeManager = orderTypeManager;
            this.officeManager = officeManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = new OrderViewModel();
            //{
            //    OrderType = await orderTypeManager.GetForDropDown(),
            //    Office = await officeManager.GetForDropDown(),
            //    Country = await officeManager.GetForDropDown(),
            //    Currency = await currencyManager.GetForDropDown(),
            //};

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OrderViewModel vModel)
        {
            var result = await orderManager.Add(vModel.Order);
            if (result.Error)
                return View(vModel);
            else
                return RedirectToAction(nameof(Index));
        }

        public IActionResult GetDataTable()
        {
            var result = new DataTableResult()
            {
                Draw = 1,
                RecordsTotal = 100,
                RecordsFiltered = 98,
                Data = "",
            };
            return Ok(result);
        }
    }
}
