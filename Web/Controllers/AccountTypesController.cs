using Business.Abstract;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Entities;
using Data.ViewModels;
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
    public class AccountTypesController : Controller
    {
        private readonly IAccountTypeManager accountTypeManager;

        public AccountTypesController(IAccountTypeManager accountTypeManager)
        {
            this.accountTypeManager = accountTypeManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new AccountTypeModel() : await accountTypeManager.GetModelByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountType accountType)
        {
            Core.Utility.Result result;
            if (accountType.Id == 0)
            {
                result = await accountTypeManager.Add(accountType);
            }
            else
            {
                result = await accountTypeManager.Update(accountType);
            }

            return Ok(result);
        }

        public async Task<IActionResult> Delete(AccountType accountType)
        {
            var result = await accountTypeManager.Delete(accountType);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await accountTypeManager.GetForDataTable(param));
        }
    }
}
