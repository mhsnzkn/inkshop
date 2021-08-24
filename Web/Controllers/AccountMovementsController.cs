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
    [Authorize(Roles = UserRoles.Admin+","+UserRoles.Accountant)]
    public class AccountMovementsController : Controller
    {
        private readonly IAccountMovementManager accountMovementManager;

        public AccountMovementsController(IAccountMovementManager accountMovementManager)
        {
            this.accountMovementManager = accountMovementManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new AccountMovement() : await accountMovementManager.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountMovement accountMovement)
        {
            Core.Utility.Result result = null;
            if (accountMovement.Id == 0)
            {
                result = await accountMovementManager.Add(accountMovement);
            }
            else
            {
                result = await accountMovementManager.Update(accountMovement);
            }

            return Ok(result);
        }

        public async Task<IActionResult> Delete(AccountMovement accountMovement)
        {
            var result = await accountMovementManager.Delete(accountMovement);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await accountMovementManager.GetForDataTable(param));
        }
    }
}
