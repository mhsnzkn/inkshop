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
    public class AccountEntitiesController : Controller
    {
        private readonly IAccountEntityManager accountEntityManager;

        public AccountEntitiesController(IAccountEntityManager accountEntityManager)
        {
            this.accountEntityManager = accountEntityManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new AccountEntity() : await accountEntityManager.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountEntity accountEntity)
        {
            Core.Utility.Result result = null;
            if (accountEntity.Id == 0)
            {
                result = await accountEntityManager.Add(accountEntity);
            }
            else
            {
                result = await accountEntityManager.Update(accountEntity);
            }

            return Ok(result);
        }

        public async Task<IActionResult> Delete(AccountEntity accountEntity)
        {
            var result = await accountEntityManager.Delete(accountEntity);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await accountEntityManager.GetForDataTable(param));
        }
    }
}
