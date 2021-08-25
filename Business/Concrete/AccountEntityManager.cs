using AutoMapper;
using Business.Abstract;
using Core.Utility;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Entities;
using Data.ViewModels;
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
    public class AccountEntityManager : IAccountEntityManager
    {
        private readonly IAccountEntityDal entityDal;
        private readonly IMapper mapper;

        public AccountEntityManager(IAccountEntityDal entityDal, IMapper mapper)
        {
            this.entityDal = entityDal;
            this.mapper = mapper;
        }
        
        public async Task<List<AccountEntity>> Get(System.Linq.Expressions.Expression<Func<AccountEntity, bool>> expression = null)
        {
            return await entityDal.Get(expression).ToListAsync();
        }

        public async Task<AccountEntity> GetByIdAsync(int id)
        {
            AccountEntity result = null;
            try
            {
                result = await entityDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<AccountEntityModel> GetModelByIdAsync(int id)
        {
            AccountEntityModel result = null;
            try
            {
                result = mapper.Map<AccountEntityModel>(await entityDal.GetByIdAsync(id));
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(AccountEntity entity)
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

        public async Task<Result> Delete(AccountEntity entity)
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

        public async Task<Result> Update(AccountEntity entity)
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
            return await entityDal.Get().OrderBy(a => a.Name).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();
        }

        public async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = entityDal.Get();
            // Filter
            if (!string.IsNullOrEmpty(param.search?.value))
            {
                query = query.Where(a => a.Name.Contains(param.search.value) || a.City.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }

            var list = await query.OrderBy(a => a.Name).Skip(param.start).Take(param.length).ToListAsync();

            // DataTableModel
            result.Data = list;
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
