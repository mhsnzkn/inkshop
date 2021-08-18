using AutoMapper;
using Business.Abstract;
using Core.Utility;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Dtos;
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
    public class VaultManager : IVaultManager
    {
        private readonly IOrderDal orderDal;
        private readonly IMapper mapper;
        private readonly IOrderPersonnelDal orderPersonnelDal;

        public VaultManager(IOrderDal orderDal, IMapper mapper, IOrderPersonnelDal orderPersonnelDal)
        {
            this.orderDal = orderDal;
            this.mapper = mapper;
            this.orderPersonnelDal = orderPersonnelDal;
        }

        public async Task<DataTableResult> GetIncomeDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = orderDal.Get(a=>a.Deposit > 0).OrderBy(a => a.CurrencyId)
                .Include(a => a.Office).Include(a => a.Currency)
                .AsQueryable();

            if (param.minDate != null)
                query = query.Where(a => a.Date.Date >= param.minDate);
            if (param.maxDate != null)
                query = query.Where(a => a.Date.Date <= param.maxDate);

            var paginatedQuery = query.Skip(param.start).Take(param.length);
            var list = await mapper.ProjectTo<VaultIncomeDto>(paginatedQuery).ToListAsync();

            // DataTableModel
            result.Data = list;
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }

        public async Task<DataTableResult> GetExpenseDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = orderPersonnelDal.Get(a => a.Order.IsApproved == true).OrderBy(a => a.Order.CurrencyId)
                .Include(a=>a.Personnel).Include(a=>a.Order).Include(a => a.Order.Office).Include(a => a.Order.Currency)
                .AsQueryable();

            //var query = orderDal.Get(a=>a.IsApproved == true).OrderBy(a => a.CurrencyId)
            //    .Include(a => a.Office).Include(a => a.Currency)
            //    .AsQueryable();

            if (param.minDate != null)
                query = query.Where(a => a.Order.ApproveDate.Value.Date >= param.minDate);
            if (param.maxDate != null)
                query = query.Where(a => a.Order.ApproveDate.Value.Date <= param.maxDate);

            var paginatedQuery = query.Skip(param.start).Take(param.length);

            // DataTableModel
            result.Data = await mapper.ProjectTo<VaultExpenseDto>(paginatedQuery).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
