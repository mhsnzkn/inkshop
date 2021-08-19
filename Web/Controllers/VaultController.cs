using Business.Abstract;
using Core.Utility.Datatables;
using Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize(Roles = UserRoles.Admin+","+UserRoles.Accountant)]
    public class VaultController : Controller
    {
        private readonly IVaultManager vaultManager;

        public VaultController(IVaultManager vaultManager)
        {
            this.vaultManager = vaultManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Income()
        {
            return View();
        }
        public IActionResult Expense()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetIncomeDataTable([FromBody] DataTableParams param)
        {
            DataTableResult result = new DataTableResult();
            try
            {
                result = await vaultManager.GetIncomeDataTable(param);
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetExpenseDataTable([FromBody] DataTableParams param)
        {
            DataTableResult result = new DataTableResult();
            try
            {
                result = await vaultManager.GetExpenseDataTable(param);
            }
            catch (Exception ex)
            {
                result.Error = ex.ToString();
            }
            return Ok(result);
        }
    }
}
