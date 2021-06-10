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
            var model = new Country();
            //{
            //    OrderType = await orderTypeManager.GetForDropDown(),
            //    Office = await officeManager.GetForDropDown(),
            //    Country = await officeManager.GetForDropDown(),
            //    Currency = await currencyManager.GetForDropDown(),
            //};

            return PartialView("_Edit", model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Country country)
        {
            var result = await countryManager.Add(country);
            return Ok(result);
        }

        public async Task<IActionResult> GetDataTable()
        {
            var a = HttpContext.Request;
            var result = new DataTableResult()
            {
                Draw = 1,
                RecordsTotal = 10,
                RecordsFiltered = 8,
                Data = await countryManager.Get(),
            };
            return Ok(result);
        }
    }
}
