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
                entity.Type = dto.TypeCoverUp ? OrderTypeString.CoverUp : string.Empty;
                entity.Type += dto.TypeFreeHand ? OrderTypeString.Freehand : string.Empty;
                entity.Type += dto.TypeRefresh ? OrderTypeString.Refresh : string.Empty;
                entity.Type += dto.TypeTouchUp ? OrderTypeString.TouchUp : string.Empty;

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
                var entity = await orderDal.GetByIdAsync(dto.Id);
                entity.OfficeId = dto.OfficeId;
                entity.OrderTypeId = dto.OrderTypeId;
                entity.CustomerCountryId = dto.CustomerCountryId;
                entity.CustomerName = dto.CustomerName;
                entity.CustomerSurname = dto.CustomerSurname;
                entity.CustomerAdress = dto.CustomerAdress;
                entity.Price = dto.Price;
                entity.Deposit = dto.Deposit;
                entity.CurrencyId = dto.CurrencyId;
                entity.CurrencyId = dto.CurrencyId;
                entity.Description = dto.Description;
                entity.Date = dto.Date;
                entity.IsCreditCard = dto.IsCreditCard;

                entity.Type = dto.TypeCoverUp ? OrderTypeString.CoverUp : string.Empty;
                entity.Type += dto.TypeFreeHand ? OrderTypeString.Freehand : string.Empty;
                entity.Type += dto.TypeRefresh ? OrderTypeString.Refresh : string.Empty;
                entity.Type += dto.TypeTouchUp ? OrderTypeString.TouchUp : string.Empty;
                entity.UptDate = DateTime.Now;

                orderDal.Update(entity);
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> OrderApprove(int id)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(id);
                entity.IsOrderApproved = true;
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> OrderCancel(int id, string message)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(id);
                entity.IsOrderApproved = false;
                entity.OrderCancellationReason = message;
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
            var query = orderDal.Get()
                .Include(a => a.Office).Include(a => a.Currency).Include(a => a.CustomerCountry).Include(a => a.OrderType)
                .Skip(param.start).Take(param.length);

            // Filter
            if (!string.IsNullOrEmpty(param.search.value))
            {
                query = query.Where(a => a.CustomerName.Contains(param.search.value) || a.CustomerSurname.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }

            if(param.search.listCancelled != null)
            {
                query = query.Where(a => a.IsOrderApproved == !param.search.listCancelled);
            }
            else
            {
                query = query.Where(a => a.IsOrderApproved == null);
            }
            

            // DataTableModel
            result.Data = mapper.Map<List<OrderTableDto>>(await query.ToListAsync());
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;
        }
    }
}
