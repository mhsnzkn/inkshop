using AutoMapper;
using Business.Abstract;
using Core.Utility;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Entities;
using Data.ViewModels;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Business.Generic;

namespace Business.Concrete
{
    public class AccountEntityManager : DefinitionManager<AccountEntity, IAccountEntityDal>, IAccountEntityManager
    {
        private readonly IAccountEntityDal entityDal;
        private readonly IMapper mapper;

        public AccountEntityManager(IAccountEntityDal entityDal, IMapper mapper) : base(entityDal)
        {
            this.entityDal = entityDal;
            this.mapper = mapper;
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

        public async Task<Result> Add(AccountEntityModel model)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<AccountEntity>(model);
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

        public async Task<Result> Update(AccountEntityModel model)
        {
            var result = new Result();
            try
            {
                var entityFromDb = await entityDal.GetByIdAsync(model.Id);
                entityFromDb.Name = model.Name;
                entityFromDb.Mail = model.Mail;
                entityFromDb.Address = model.Address;
                entityFromDb.City = model.City;
                entityFromDb.Description = model.Description;
                entityFromDb.Phone = model.Phone;

                entityFromDb.UptDate = DateTime.Now;
                entityDal.Update(entityFromDb);
                await entityDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public override async Task<DataTableResult> GetForDataTable(DataTableParams param)
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
