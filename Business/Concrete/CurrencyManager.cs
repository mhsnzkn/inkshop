using Business.Abstract;
using Core.Utility;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Entities;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CurrencyManager : ICurrencyManager
    {
        private readonly ICurrencyDal currencyDal;

        public CurrencyManager(ICurrencyDal currencyDal)
        {
            this.currencyDal = currencyDal;
        }
        
        public async Task<List<Currency>> Get(System.Linq.Expressions.Expression<Func<Currency, bool>> expression = null)
        {
            return await currencyDal.Get(expression).ToListAsync();
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            Currency result = null;
            try
            {
                result = await currencyDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(Currency entity)
        {
            var result = new Result();
            try
            {
                currencyDal.Add(entity);
                await currencyDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> Delete(Currency entity)
        {
            var result = new Result();
            try
            {
                currencyDal.Delete(entity);
                await currencyDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> Update(Currency entity)
        {
            var result = new Result();
            try
            {
                currencyDal.Update(entity);
                await currencyDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }
        public async Task<List<SelectListItem>> GetForDropDown()
        {
            return await currencyDal.Get().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();
        }

        public async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            // Filter
            Expression<Func<Currency, bool>> exp = null;
            if (!string.IsNullOrEmpty(param.search.value))
            {
                exp = a => a.Name.Contains(param.search.value) || a.ShortName.Contains(param.search.value) || a.Description.Contains(param.search.value);
            }

            // DataTableModel
            result.Data = await currencyDal.Get(exp).Skip(param.start).Take(param.length).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await currencyDal.Get(exp).CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
