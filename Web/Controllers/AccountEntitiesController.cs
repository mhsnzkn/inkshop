using AutoMapper;
using Business.Abstract;
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
    public class AccountEntitiesController : Controller
    {
        private readonly IAccountEntityManager accountEntityManager;
        private readonly IMapper mapper;

        public AccountEntitiesController(IAccountEntityManager accountEntityManager, IMapper mapper)
        {
            this.accountEntityManager = accountEntityManager;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new AccountEntityModel() : await accountEntityManager.GetModelByIdAsync(id);

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

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]AccountEntity accountEntity)
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
