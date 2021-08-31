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
    public class OfficesController : Controller
    {
        private readonly IOfficeManager officeManager;

        public OfficesController(IOfficeManager officeManager)
        {
            this.officeManager = officeManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new Office() : await officeManager.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Office office)
        {
            Core.Utility.Result result = null;
            if (office.Id == 0)
            {
                result = await officeManager.Add(office);
            }
            else
            {
                result = await officeManager.Update(office);
            }
            if (result.Error)
                return View(office);
            else
                return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Office office)
        {
            var result = await officeManager.Delete(office);
            if (result.Error)
                return View("Edit", office);
            else
                return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetForDropDown()
        {
            var items = await officeManager.GetForDropDown();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await officeManager.GetForDataTable(param));
        }
    }
}
