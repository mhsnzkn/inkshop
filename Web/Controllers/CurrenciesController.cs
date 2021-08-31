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
    public class CurrenciesController : Controller
    {
        private readonly ICurrencyManager currencyManager;

        public CurrenciesController(ICurrencyManager currencyManager)
        {
            this.currencyManager = currencyManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new Currency() : await currencyManager.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Currency currency)
        {
            Core.Utility.Result result = null;
            if (currency.Id == 0)
            {
                result = await currencyManager.Add(currency);
            }
            else
            {
                result = await currencyManager.Update(currency);
            }
            if (result.Error)
                return View(currency);
            else
                return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Currency currency)
        {
            var result = await currencyManager.Delete(currency);
            if (result.Error)
            {
                ViewData["error"] = result.Message;
                return View("Edit", currency);
            }
            else
                return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetForDropDown()
        {
            var items = await currencyManager.GetForDropDown();
            return Ok(items);
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await currencyManager.GetForDataTable(param));
        }
    }
}
