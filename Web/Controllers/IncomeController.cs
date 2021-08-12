using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IOrderManager orderManager;

        public IncomeController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
