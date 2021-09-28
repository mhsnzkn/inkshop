using Business.Abstract;
using Core.Data;
using Core.DataAccess;
using Core.Utility;
using Core.Utility.Datatables;
using Data.Abstract;
using Data.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Generic
{
    public abstract class DefinitionManager<TEntity, TDal> : IDefinitionManager<TEntity>
        where TEntity : DefinitionEntity, new()
        where TDal : IRepositoryBase<TEntity>
    {
        private readonly TDal entityDal;
        public DefinitionManager(TDal entityDal)
        {
            this.entityDal = entityDal;
        }

        public async Task<List<TEntity>> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression = null)
        {
            return await entityDal.Get(expression).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            TEntity result = null;
            try
            {
                result = await entityDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(TEntity entity)
        {
            var result = new Result();
            try
            {
                entityDal.Add(entity);
                await entityDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> Delete(TEntity entity)
        {
            var result = new Result();
            try
            {
                entityDal.Delete(entity);
                await entityDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> Update(TEntity entity)
        {
            var result = new Result();
            try
            {
                entityDal.Update(entity);
                await entityDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }
        public virtual async Task<List<SelectListItem>> GetForDropDown()
        {
            return await entityDal.Get().OrderBy(a => a.Name).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();
        }

        public virtual async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = entityDal.Get();
            // Filter
            if (!string.IsNullOrEmpty(param.search?.value))
            {
                query = query.Where(a => a.Name.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }

            var totalCount = await query.CountAsync();
            if (param.length > 0)
            {
                query = query.Skip(param.start).Take(param.length);
            }
            var list = await query.OrderBy(a => a.Name).ToListAsync();

            // DataTableModel
            result.Data = list;
            result.Draw = param.draw;
            result.RecordsTotal = totalCount;
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
