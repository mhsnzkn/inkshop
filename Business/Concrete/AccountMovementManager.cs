using AutoMapper;
using Business.Abstract;
using Core.Utility;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Dtos;
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
                entity.Description = model.Description;
                entity.VaultInId = model.VaultInId;
                entity.VaultOutId = model.VaultOutId;

                entity.UptDate = DateTime.Now;
                await entityDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<DataTableResult> GetForDataTable(AccountParamsDto param)
        {
            var result = new DataTableResult();
            var query = GetFilteredQuery(param);

            var paginatedQuery = query.OrderByDescending(a => a.Date).Skip(param.start).Take(param.length);

            // DataTableModel
            result.Data = mapper.ProjectTo<AccountMovementTableDto>(paginatedQuery);
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }

        public async Task<List<AccountMovementSumDto>> GetSummary(AccountParamsDto param)
        {
            var query = GetFilteredQuery(param);

            var result = await query.GroupBy(a => a.Currency.Name)
                .Select(a => new AccountMovementSumDto
            {
                CurrencyName = a.Key,
                Income = a.Sum(s => s.Income),
                Expense = a.Sum(s => s.Expense)
            }).ToListAsync();

            return result;

        }
        public async Task<List<AccountMovementSumDto>> GetVaultSummary(AccountParamsDto param)
        {
            var query = GetFilteredQuery(param);
            if(param.VaultId == 0)
            {
                query = query.Where(a => a.VaultInId != null || a.VaultOutId != null);
            }
            var queryResult = await query.Include(a=>a.VaultIn).Include(a=>a.VaultOut).Include(a=>a.Currency).ToListAsync();
            var vaults = queryResult.Select(a => a.VaultIn).GroupBy(a=>a.Id).Select(a=>a.First());
            var currencies = queryResult.Select(a => a.Currency).GroupBy(a => a.Id).Select(a => a.First());
            var result = new List<AccountMovementSumDto>();
            foreach (var vault in vaults)
            {
                for (int i = 0; i < currencies.Count(); i++)
                {
                    var element = currencies.ElementAt(i);
                    var item = new AccountMovementSumDto()
                    {
                        CurrencyName = element.Name,
                        VaultName = vault.Name + " " + vault.Description,
                        Income = queryResult.Where(a => a.VaultInId == vault.Id && a.CurrencyId == element.Id).Sum(a => a.Income),
                        Expense = queryResult.Where(a => a.VaultOutId == vault.Id && a.CurrencyId == element.Id).Sum(a => a.Expense),
                    };

                    if(item.Income > 0 || item.Expense > 0)
                        result.Add(item);
                }
            }

            return result;

        }

        private IQueryable<AccountMovement> GetFilteredQuery(AccountParamsDto param)
        {
            var query = entityDal.Get();
            // Filter
            if (!string.IsNullOrEmpty(param.search?.value))
            {
                query = query.Where(a => a.Entity.Name.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }
            if (param.OfficeId > 0)
                query = query.Where(a => a.OfficeId == param.OfficeId);
            if (param.EntityId > 0)
                query = query.Where(a => a.EntityId == param.EntityId);
            if (param.TypeId > 0)
                query = query.Where(a => a.TypeId == param.TypeId);
            if (param.VaultId > 0)
                query = query.Where(a => a.VaultInId == param.VaultId || a.VaultOutId==param.VaultId);

            if (param.minDate != null)
                query = query.Where(a => a.Date.Date >= param.minDate);
            if (param.maxDate != null)
                query = query.Where(a => a.Date.Date <= param.maxDate);

            return query;
        }
    }
}
