using Business.Abstract;
using Data.Dtos;
using Core.Utility;
using Data.Entities;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Constants;
using Core.Utility.Datatables;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderDal orderDal;
        private readonly IMapper mapper;

        public OrderManager(IOrderDal orderDal, IMapper mapper)
        {
            this.orderDal = orderDal;
            this.mapper = mapper;
        }
        
        public async Task<List<Order>> Get(System.Linq.Expressions.Expression<Func<Order, bool>> expression = null)
        {
            return await orderDal.Get(expression).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            Order result = null;
            try
            {
                result = await orderDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(OrderAddDto dto)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Order>(dto);
                entity.CrtDate = DateTime.Now;
                entity.Type = dto.TypeCoverUp ? OrderTypeString.CoverUp : "";
                entity.Type += dto.TypeFreeHand ? OrderTypeString.Freehand : entity.Type;
                entity.Type += dto.TypeRefresh ? OrderTypeString.Refresh : entity.Type;
                entity.Type += dto.TypeTouchUp ? OrderTypeString.TouchUp : entity.Type;

                orderDal.Add(entity);
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> Update(OrderAddDto dto)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Order>(dto);
                entity.UptDate = DateTime.Now;
                entity.Type = dto.TypeCoverUp ? OrderTypeString.CoverUp : "";
                entity.Type += dto.TypeFreeHand ? OrderTypeString.Freehand : entity.Type;
                entity.Type += dto.TypeRefresh ? OrderTypeString.Refresh : entity.Type;
                entity.Type += dto.TypeTouchUp ? OrderTypeString.TouchUp : entity.Type;

                orderDal.Update(entity);
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> Delete(Order entity)
        {
            var result = new Result();
            try
            {
                orderDal.Delete(entity);
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            // Filter
            Expression<Func<Order, bool>> exp = null;
            if (!string.IsNullOrEmpty(param.search.value))
            {
                exp = a => a.CustomerName.Contains(param.search.value) || a.CustomerSurname.Contains(param.search.value) || a.Description.Contains(param.search.value);
            }

            // DataTableModel
            result.Data = mapper.Map<List<OrderTableDto>>(await orderDal.Get(exp)
                .Include(a=>a.Office).Include(a=>a.Currency).Include(a=>a.CustomerCountry).Include(a=>a.OrderType)
                .Skip(param.start).Take(param.length).ToListAsync());
            result.Draw = param.draw;
            result.RecordsTotal = await orderDal.Get(exp).CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;
        }
    }
}
