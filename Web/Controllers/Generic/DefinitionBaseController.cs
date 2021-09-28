using Core.Data;
using Core.Utility.Datatables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Business.Generic;
using Data.Abstract;

namespace Web.Controllers.Generic
{
    public class DefinitionBaseController<TEntity, TManager> : Controller
        where TEntity : DefinitionEntity, IEntity, new()
        where TManager : IDefinitionManager<TEntity>
    {
        private readonly TManager entityManager;

        public DefinitionBaseController(TManager entityManager)
        {
            this.entityManager = entityManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = id == 0 ? new TEntity() : await entityManager.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TEntity entity)
        {
            Core.Utility.Result result = null;
            if (entity.Id == 0)
            {
                result = await entityManager.Add(entity);
            }
            else
            {
                result = await entityManager.Update(entity);
            }

            return Ok(result);
        }

        public async Task<IActionResult> Delete(TEntity entity)
        {
            var result = await entityManager.Delete(entity);

            return Ok(result);
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetForDropDown()
        {
            var items = await entityManager.GetForDropDown();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataTable([FromBody] DataTableParams param)
        {
            return Ok(await entityManager.GetForDataTable(param));
        }
    }
}
