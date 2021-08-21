using Business.Abstract;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
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
            if(orderType.Id == OrderTypeId.Dovme.GetHashCode() || orderType.Id == OrderTypeId.MakePiercing.GetHashCode())
            {
                result = new Core.Utility.Result();
                result.SetError(UserMessages.CannotChange, UserMessages.CannotChange);
                return Ok(result);
            }

            if (orderType.Id == 0)
                result = await orderTypeManager.Add(orderType);
            else
                result = await orderTypeManager.Update(orderType);

            return Ok(result);
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

        public async Task<IActionResult> GetForDropDown()
        {
            var items = await orderTypeManager.GetForDropDown();
            return Ok(items);
        }
    }
}
