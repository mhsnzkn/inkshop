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
    public class AccountMovementManager : IAccountMovementManager
    {
        private readonly IAccountMovementDal entityDal;
        private readonly IMapper mapper;

        public AccountMovementManager(IAccountMovementDal entityDal, IMapper mapper)
        {
            this.entityDal = entityDal;
            this.mapper = mapper;
        }
        
        public async Task<List<AccountMovement>> Get(Expression<Func<AccountMovement, bool>> expression = null)
        {
            return await entityDal.Get(expression).ToListAsync();
        }

        public async Task<AccountMovement> GetByIdAsync(int id)
        {
            AccountMovement result = null;
            try
            {
                result = await entityDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<AccountMovementModel> GetModelByIdAsync(int id)
        {
            AccountMovementModel result = null;
            try
            {
                result = mapper.Map<AccountMovementModel>(await entityDal.GetByIdAsync(id));
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(AccountMovementModel model)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<AccountMovement>(model);
                entity.CrtDate = DateTime.Now;
                entityDal.Add(entity);
                await entityDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> Delete(AccountMovement entity)
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

        public async Task<Result> Update(AccountMovementModel model)
        {
            var result = new Result();
            try
            {
                var entity = await entityDal.GetByIdAsync(model.Id);
                entity.OfficeId = model.OfficeId;
                entity.EntityId = model.EntityId;
                entity.TypeId = model.TypeId;
                entity.CurrencyId = model.CurrencyId;
                entity.Income = model.Income;
                entity.Expense = model.Expense;
                entity.Date = model.Date;
                entity.DueDate = model.DueDate;

                entity.UptDate = DateTime.Now;
                await entityDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = entityDal.Get();
            // Filter
            if (!string.IsNullOrEmpty(param.search?.value))
            {
                query = query.Where(a => a.Entity.Name.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }
            var paginatedQuery = query.OrderByDescending(a => a.Date).Skip(param.start).Take(param.length);

            // DataTableModel
            result.Data = mapper.ProjectTo<AccountMovementTableDto>(paginatedQuery);
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
