using Business.Abstract;
using Core.Utility.Datatables;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class OrderTypesController : Controller
    {
        private readonly IOrderTypeManager orderTypeManager;

        public OrderTypesController(IOrderTypeManager orderTypeManager)
        {
            this.orderTypeManager = orderTypeManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new OrderType() : await orderTypeManager.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrderType orderType)
        {
            Core.Utility.Result result = null;
            if (orderType.Id == 0)
            {
                result = await orderTypeManager.Add(orderType);
            }
            else
            {
                result = await orderTypeManager.Update(orderType);
            }
            if (result.Error)
                return View(orderType);
            else
                return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(OrderType orderType)
        {
            var result = await orderTypeManager.Delete(orderType);
            if (result.Error)
                return View("Edit", orderType);
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await orderTypeManager.GetForDataTable(param));
        }
    }
}
