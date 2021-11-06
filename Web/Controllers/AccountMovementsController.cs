﻿using Business.Abstract;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Dtos;
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
            var model = id == 0 ? new AccountMovementModel() : await accountMovementManager.GetModelByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountMovementModel accountMovement)
        {
            Core.Utility.Result result;
            if(accountMovement.Income == 0 && accountMovement.Expense == 0)
            {
                result = new Core.Utility.Result();
                result.SetError("Gelir ve Gider 0 olamaz", "Gelir ve Gider 0 olamaz");
                return Ok(result);
            }
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
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]AccountMovement accountMovement)
        {
            var result = await accountMovementManager.Delete(accountMovement);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] AccountParamsDto param)
        {
            return Ok(await accountMovementManager.GetForDataTable(param));
        }
        [HttpPost]
        public async Task<IActionResult> GetSummary([FromBody] AccountParamsDto param)
        {
            var result = new
            {
                general = await accountMovementManager.GetSummary(param),
                vault = await accountMovementManager.GetVaultSummary(param)
            };

            return Ok(result);
        }
    }
}
