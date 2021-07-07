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
    public class PersonnelController : Controller
    {
        private readonly IPersonnelManager personnelManager;

        public PersonnelController(IPersonnelManager personnelManager)
        {
            this.personnelManager = personnelManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new Personnel() : await personnelManager.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personnel personnel)
        {
            Core.Utility.Result result = null;
            if (personnel.Id == 0)
            {
                result = await personnelManager.Add(personnel);
            }
            else
            {
                result = await personnelManager.Update(personnel);
            }
            if (result.Error)
            {
                ViewData["error"] = result.Message;
                return View(personnel);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Personnel personnel)
        {
            var result = await personnelManager.Delete(personnel);
            if (result.Error)
                return View("Edit", personnel);
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await personnelManager.GetForDataTable(param));
        }
        public async Task<IActionResult> GetForDropDown()
        {
            var items = await personnelManager.GetForDropDown();
            return Ok(items);
        }
    }
}
