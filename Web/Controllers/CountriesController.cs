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
    public class CountriesController : Controller
    {
        private readonly ICountryManager countryManager;

        public CountriesController(ICountryManager countryManager)
        {
            this.countryManager = countryManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new Country() : await countryManager.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Country country)
        {
            Core.Utility.Result result = null;
            if (country.Id == 0)
            {
                result = await countryManager.Add(country);
            }
            else
            {
                result = await countryManager.Update(country);
            }
            if (result.Error)
                return View(country);
            else
                return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Country country)
        {
            var result = await countryManager.Delete(country);
            if (result.Error)
                return View("Edit", country);
            else
                return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetDataTable()
        {
            var a = HttpContext.Request;
            //var result = new DataTableResult()
            //{
            //    Data = await countryManager.Get(),
            //};
            return Ok(new { data = await countryManager.Get() });
        }
    }
}
