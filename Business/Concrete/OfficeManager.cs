﻿using Business.Abstract;
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
                result.SetError(ex.ToString(), UserMessages.Fail);
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
                result.SetError(ex.ToString(), UserMessages.Fail);
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
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }
        public async Task<List<SelectListItem>> GetForDropDown()
        {
            return await officeDal.Get().OrderBy(a => a.Name).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();
        }

        public async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = officeDal.Get();
            // Filter
            if (!string.IsNullOrEmpty(param.search.value))
            {
                query = query.Where(a => a.Name.Contains(param.search.value) || a.City.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }
            if (param.length > 0)
            {
                query = query.Skip(param.start).Take(param.length);
            }
            var list = await query.OrderBy(a => a.Name).ToListAsync();

            // DataTableModel
            result.Data = list;
            result.Draw = param.draw;
            result.RecordsTotal = list.Count;
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
