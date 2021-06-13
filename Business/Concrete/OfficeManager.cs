using Business.Abstract;
using Core.Utility;
using Core.Utility.Datatables;
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
    public class OfficeManager : IOfficeManager
    {
        private readonly IOfficeDal officeDal;

        public OfficeManager(IOfficeDal officeDal)
        {
            this.officeDal = officeDal;
        }
        
        public async Task<List<Office>> Get(System.Linq.Expressions.Expression<Func<Office, bool>> expression = null)
        {
            return await officeDal.Get(expression).ToListAsync();
        }

        public async Task<Office> GetByIdAsync(int id)
        {
            Office result = null;
            try
            {
                result = await officeDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(Office entity)
        {
            var result = new Result();
            try
            {
                officeDal.Add(entity);
                await officeDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> Delete(Office entity)
        {
            var result = new Result();
            try
            {
                officeDal.Delete(entity);
                await officeDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> Update(Office entity)
        {
            var result = new Result();
            try
            {
                officeDal.Update(entity);
                await officeDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }
        public async Task<List<SelectListItem>> GetForDropDown()
        {
            return await officeDal.Get().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();
        }

        public async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            // Filter
            Expression<Func<Office, bool>> exp = null;
            if (!string.IsNullOrEmpty(param.search.value))
            {
                exp = a => a.Name.Contains(param.search.value) || a.City.Contains(param.search.value) || a.Description.Contains(param.search.value);
            }

            // DataTableModel
            result.Data = await officeDal.Get(exp).Skip(param.start).Take(param.length).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await officeDal.Get(exp).CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
