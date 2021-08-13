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

        public VaultManager(IOrderDal orderDal, IMapper mapper)
        {
            this.orderDal = orderDal;
            this.mapper = mapper;
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
            //List<VaultIncomeDto> resultList = new();
            //foreach (var currency in list.Select(a=>a.CurrencyId).Distinct())
            //{
            //    var currencyList = list.Where(a => a.CurrencyId == currency);
            //    resultList.AddRange(currencyList);
            //    resultList.Add(new VaultIncomeDto { CurrencyId = 0, CustomerFullName = "TOPLAM", Price = currencyList.Sum(a => a.Deposit) });
            //}

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
            var query = orderDal.Get().OrderBy(a => a.Date)
                .Include(a => a.Office).Include(a => a.Currency)
                .AsQueryable();

            if (param.minDate != null)
                query = query.Where(a => a.Date.Date >= param.minDate);
            if (param.maxDate != null)
                query = query.Where(a => a.Date.Date <= param.maxDate);

            var paginatedQuery = query.Skip(param.start).Take(param.length);

            // DataTableModel
            result.Data = await mapper.ProjectTo<ReservationTableDto>(paginatedQuery).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
