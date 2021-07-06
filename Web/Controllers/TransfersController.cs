using AutoMapper;
using Business.Abstract;
using Core.Utility.Datatables;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class TransfersController : Controller
    {
        private readonly IOrderManager orderManager;
        private readonly IOfficeManager officeManager;
        private readonly ICountryManager countryManager;
        private readonly IMapper mapper;

        public TransfersController(IOrderManager orderManager,
            IOfficeManager officeManager, ICountryManager countryManager,
            IMapper mapper)
        {
            this.orderManager = orderManager;
            this.officeManager = officeManager;
            this.countryManager = countryManager;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = new TransferViewModel()
            {
                Country = await countryManager.GetForDropDown(),
            };
            model.Transfer = id == 0 ? new TransferDto() : mapper.Map<TransferDto>(await orderManager.GetByIdAsync(id));

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TransferViewModel vModel)
        {
            Core.Utility.Result result = null;
            if(vModel.Transfer.Id == 0)
            {
                result = await orderManager.AddTransfer(vModel.Transfer);
            }
            else
            {
                result = await orderManager.UpdateTransfer(vModel.Transfer);
            }

            return Ok(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> Cancel([FromBody] OrderPostDto dto)
        {
            if (dto == null || dto.Id == 0)
                return BadRequest();

            return Ok(await orderManager.CancelOrder(dto.Id,dto.Message ));
        }

        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await orderManager.GetTransferDataTable(param));
        }
    }
}
