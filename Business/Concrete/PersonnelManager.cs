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
    public class PersonnelManager : IPersonnelManager
    {
        private readonly IPersonnelDal entityDal;

        public PersonnelManager(IPersonnelDal entityDal)
        {
            this.entityDal = entityDal;
        }
        
        public async Task<List<Personnel>> Get(System.Linq.Expressions.Expression<Func<Personnel, bool>> expression = null)
        {
            return await entityDal.Get(expression).ToListAsync();
        }

        public async Task<Personnel> GetByIdAsync(int id)
        {
            Personnel result = null;
            try
            {
                result = await entityDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(Personnel entity)
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

        public async Task<Result> Delete(Personnel entity)
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

        public async Task<Result> Update(Personnel entity)
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
        public async Task<List<SelectListItem>> GetForDropDown()
        {
            return await entityDal.Get().Select(a => new SelectListItem
            {
                Text = a.Name+" "+a.Surname+"-"+a.job,
                Value = a.Id.ToString()
            }).ToListAsync();
        }

        public async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = entityDal.Get();
            // Filter
            if (!string.IsNullOrEmpty(param.search.value))
            {
                query = query.Where(a => a.Name.Contains(param.search.value) || a.Surname.Contains(param.search.value));
            }
            IQueryable<Personnel> paginatedQuery = null;
            if (param.length > 0)
            {
                paginatedQuery = query.Skip(param.start).Take(param.length);
            }
            else
            {
                paginatedQuery = query;
            }

            // DataTableModel
            result.Data = await paginatedQuery.ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
