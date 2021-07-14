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

        public async Task<Result> AddOrder(OrderAddDto dto)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Order>(dto);
                entity.CrtDate = DateTime.Now;
                entity.Status = OrderStatus.Order;
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

        public async Task<Result> UpdateOrder(OrderAddDto dto)
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
                entity.CustomerHotel = dto.CustomerHotel;
                entity.CustomerRoomNumber = dto.CustomerRoomNumber;
                entity.CustomerPhoneNumber = dto.CustomerPhoneNumber;
                entity.Price = dto.Price;
                entity.Deposit = dto.Deposit;
                entity.CurrencyId = dto.CurrencyId;
                entity.CurrencyId = dto.CurrencyId;
                entity.Description = dto.Description;
                entity.Date = dto.Date;
                entity.IsCreditCard = dto.IsCreditCard;
                entity.Status = OrderStatus.Order;

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
        public async Task<Result> UpdateTransfer(TransferDto dto)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(dto.Id);
                //entity.OfficeId = dto.OfficeId;
                //entity.OrderTypeId = dto.OrderTypeId;
                entity.CustomerCountryId = dto.CustomerCountryId;
                entity.CustomerName = dto.CustomerName;
                entity.CustomerSurname = dto.CustomerSurname;
                entity.CustomerHotel = dto.CustomerHotel;
                entity.CustomerRoomNumber = dto.CustomerRoomNumber;
                entity.CustomerPhoneNumber = dto.CustomerPhoneNumber;
                entity.Description = dto.Description;
                entity.Date = dto.Date;
                //entity.Price = dto.Price;
                //entity.Deposit = dto.Deposit;
                //entity.CurrencyId = dto.CurrencyId;
                //entity.CurrencyId = dto.CurrencyId;
                //entity.IsCreditCard = dto.IsCreditCard;

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

        public async Task<Result> AddTransfer(TransferDto dto)
        {
            var result = new Result();
            try
            {
                var entity = mapper.Map<Order>(dto);
                entity.CrtDate = DateTime.Now;
                entity.IsTransfer = true;
                entity.Status = OrderStatus.Transfer;

                orderDal.Add(entity);
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }
        public async Task<Result> ApproveOrder(int id)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(id);
                entity.Status = OrderStatus.Reservation;
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> CancelOrder(int id, string message)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(id);
                entity.IsApproved = false;
                entity.OrderCancellationReason = message;
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }
        public async Task<Result> UpdateReservation(OrderAddDto dto)
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
                entity.CustomerHotel = dto.CustomerHotel;
                entity.CustomerRoomNumber = dto.CustomerRoomNumber;
                entity.CustomerPhoneNumber = dto.CustomerPhoneNumber;
                entity.Price = dto.Price;
                entity.Deposit = dto.Deposit;
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

        public async Task<Result> ApproveReservation(int id)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(id);
                entity.IsApproved = true;
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> CancelReservation(int id, string message)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(id);
                entity.IsApproved = false;
                entity.ReservationCancellationReason = message;
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

        public async Task<DataTableResult> GetOrderDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = orderDal.Get().Where(a=>a.Status == OrderStatus.Order)
                .Include(a => a.Office).Include(a => a.Currency).Include(a => a.CustomerCountry).Include(a => a.OrderType)
                .Skip(param.start).Take(param.length);

            // Filter
            if (!string.IsNullOrEmpty(param.search.value))
            {
                query = query.Where(a => a.CustomerName.Contains(param.search.value) || a.CustomerSurname.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }

            if(param.search.listCancelled != null)
                query = query.Where(a => a.IsApproved == !param.search.listCancelled);
            else
                query = query.Where(a => a.IsApproved == null);

            if (param.minDate != null)
                query = query.Where(a => a.Date.Date >= param.minDate);
            if (param.maxDate != null)
                query = query.Where(a => a.Date.Date <= param.maxDate);

            

            // DataTableModel
            // TODO: Orderbydescending datatable'a gore duzenlenecek
            result.Data = mapper.Map<List<OrderTableDto>>(await query.OrderByDescending(a => a.Date).ToListAsync());
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;
        }
        public async Task<DataTableResult> GetTransferDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = orderDal.Get().Where(a => a.IsTransfer)
                .Include(a => a.Office).Include(a => a.Currency).Include(a => a.CustomerCountry).Include(a => a.OrderType)
                .Skip(param.start).Take(param.length);

            // Filter
            if (!string.IsNullOrEmpty(param.search.value))
            {
                query = query.Where(a => a.CustomerName.Contains(param.search.value) || a.CustomerSurname.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }

            if(param.search.listCancelled != null)
                query = query.Where(a => a.IsApproved == !param.search.listCancelled);
            else
                query = query.Where(a => a.IsApproved != false);

            if (param.minDate != null)
                query = query.Where(a => a.Date >= param.minDate);
            if (param.maxDate != null)
                query = query.Where(a => a.Date <= param.maxDate);
            if (param.maxDate == null && param.minDate == null )
                query = query.Where(a => a.Date.Date == DateTime.Today);


            

            // DataTableModel
            // TODO: Orderbydescending datatable'a gore duzenlenecek
            result.Data = await mapper.ProjectTo<TransferTableDto>(query.OrderByDescending(a => a.Date)).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;
        }
        public async Task<DataTableResult> GetReservationDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = orderDal.Get().Where(a => a.Status == OrderStatus.Reservation)
                .Include(a => a.Office).Include(a => a.Currency).Include(a => a.CustomerCountry).Include(a => a.OrderType)
                .Skip(param.start).Take(param.length);

            // Filter
            if (!string.IsNullOrEmpty(param.search?.value))
            {
                query = query.Where(a => a.CustomerName.Contains(param.search.value) || a.CustomerSurname.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }

            if(param.search.listCancelled != null)
                query = query.Where(a => a.IsApproved == !param.search.listCancelled);
            else
                query = query.Where(a => a.IsApproved == null);

            if (param.minDate != null)
                query = query.Where(a => a.Date >= param.minDate);
            if (param.maxDate != null)
                query = query.Where(a => a.Date <= param.maxDate);

            

            // DataTableModel
            result.Data = await mapper.ProjectTo<ReservationTableDto>(query).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;
        }
    }
}
