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
using Data.ViewModels;

namespace Business.Concrete
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderDal orderDal;
        private readonly IMapper mapper;
        private readonly IOrderPersonnelDal orderPersonnelDal;
        private readonly IPersonnelDal personnelDal;

        public OrderManager(IOrderDal orderDal, IMapper mapper, IOrderPersonnelDal orderPersonnelDal,
            IPersonnelDal personnelDal)
        {
            this.orderDal = orderDal;
            this.mapper = mapper;
            this.orderPersonnelDal = orderPersonnelDal;
            this.personnelDal = personnelDal;
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

        public async Task<Order> GetReservationByIdAsync(int id)
        {
            Order result = null;
            try
            {
                result = await orderDal.Get(a => a.Id == id).Include(a => a.OrderPersonnel).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> AddOrder(OrderModel dto)
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
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> UpdateOrder(OrderModel dto)
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
                entity.PersonCount = dto.PersonCount;
                entity.TicketNumber = dto.TicketNumber;
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
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }
        public async Task<Result> UpdateTransfer(TransferModel dto)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(dto.Id);
                entity.CustomerCountryId = dto.CustomerCountryId;
                entity.CustomerName = dto.CustomerName;
                entity.CustomerSurname = dto.CustomerSurname;
                entity.CustomerHotel = dto.CustomerHotel;
                entity.CustomerRoomNumber = dto.CustomerRoomNumber;
                entity.CustomerPhoneNumber = dto.CustomerPhoneNumber;
                entity.Description = dto.Description;
                entity.Date = dto.Date;
                entity.PersonCount = dto.PersonCount;
                entity.OrderTypeId = dto.OrderTypeId;

                entity.UptDate = DateTime.Now;

                orderDal.Update(entity);
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> AddTransfer(TransferModel dto)
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
                result.SetError(ex.ToString(), UserMessages.Fail);
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
                result.SetError(ex.ToString(), UserMessages.Fail);
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
                entity.CancellationReason = message;
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }
        public async Task<Result> UpdateReservation(ReservationModel dto)
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
                entity.TicketNumber = dto.TicketNumber;
                entity.IsPaymentDone = false;

                entity.Type = dto.TypeCoverUp ? OrderTypeString.CoverUp : string.Empty;
                entity.Type += dto.TypeFreeHand ? OrderTypeString.Freehand : string.Empty;
                entity.Type += dto.TypeRefresh ? OrderTypeString.Refresh : string.Empty;
                entity.Type += dto.TypeTouchUp ? OrderTypeString.TouchUp : string.Empty;
                entity.UptDate = DateTime.Now;

                await orderPersonnelDal.Upsert(entity.Id, dto.ArtistId, dto.InfoMenId, dto.MiddleMenId);

                orderDal.Update(entity);
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<Result> ApproveReservation(int id)
        {
            var result = new Result();
            try
            {
                var order = await orderDal.GetByIdAsync(id);
                order.IsApproved = true;
                order.ApproveDate = DateTime.Now;

                var personnelList = await orderPersonnelDal.Get(a => a.OrderId == order.Id).Include(a=>a.Personnel).ToListAsync();
                // Franchising Kontrolu ve Ekleme
                await orderPersonnelDal.AddFranchising(order);
                var maxPrice = 0M;
                //Hanutcu ucret hesaplama
                var hanutcu = personnelList.Where(a => a.Job == OrderPersonnelJob.Hanut).FirstOrDefault();
                orderPersonnelDal.SetPersonnelPrice(hanutcu, order, null, ref maxPrice);

                // Infocu ucret hesaplama
                var infocu = personnelList.Where(a => a.Job == OrderPersonnelJob.Info).FirstOrDefault();
                orderPersonnelDal.SetPersonnelPrice(infocu, order, null, ref maxPrice);
                // artist ucret hesaplama
                var orderPrice = order.Price - maxPrice;
                var artist = personnelList.Where(a => a.Job == OrderPersonnelJob.Artist).FirstOrDefault();
                orderPersonnelDal.SetPersonnelPrice(artist, order, orderPrice, ref maxPrice);

                // Ic Personnel ucret hesaplama
                await orderPersonnelDal.AddInternalPersonnel(order, orderPrice);


                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }
        public async Task<Result> PayReservation(int id)
        {
            var result = new Result();
            try
            {
                var entity = await orderDal.GetByIdAsync(id);
                entity.IsPaymentDone = true;
                entity.PaymentDate = DateTime.Now;
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
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
                entity.CancellationReason = message;
                await orderDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), UserMessages.Fail);
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
                result.SetError(ex.ToString(), UserMessages.Fail);
            }

            return result;
        }

        public async Task<DataTableResult> GetOrderDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var rawQuery = orderDal.Get(a => a.Status == OrderStatus.Order).OrderByDescending(a => a.Date)
                .Include(a => a.Office).Include(a => a.Currency).Include(a => a.CustomerCountry).Include(a => a.OrderType)
                .AsQueryable();

            QueryFilter(rawQuery, param, out var query, out var paginatedQuery);

            // DataTableModel
            result.Data = await mapper.ProjectTo<OrderTableDto>(paginatedQuery).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;
        }
        public async Task<DataTableResult> GetTransferDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var rawQuery = orderDal.Get(a => a.IsTransfer).OrderBy(a => a.Date)
                .Include(a => a.Office).Include(a => a.CustomerCountry)
                .AsQueryable();

            QueryFilter(rawQuery, param, out var query, out var paginatedQuery);

            // DataTableModel
            result.Data = await mapper.ProjectTo<TransferTableDto>(paginatedQuery).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;
        }
        public async Task<DataTableResult> GetReservationDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var rawQuery = orderDal.Get(a => a.Status == OrderStatus.Reservation).OrderByDescending(a => a.Date)
                .Include(a => a.Office).Include(a => a.Currency).Include(a => a.CustomerCountry).Include(a => a.OrderType)
                .Include(a => a.OrderPersonnel).ThenInclude(a => a.Personnel)
                .AsQueryable();

            QueryFilter(rawQuery, param, out var query, out var paginatedQuery);

            // DataTableModel
            result.Data = await mapper.ProjectTo<ReservationTableDto>(paginatedQuery).ToListAsync();
            result.Draw = param.draw;
            result.RecordsTotal = await query.CountAsync();
            result.RecordsFiltered = result.RecordsTotal;

            return result;
        }

        private void QueryFilter(IQueryable<Order> query, DataTableParams param, out IQueryable<Order> outQuery, out IQueryable<Order> paginatedQuery)
        {
            // Filter
            if (!string.IsNullOrEmpty(param.search?.value))
            {
                query = query.Where(a => a.CustomerName.Contains(param.search.value) || a.CustomerSurname.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }

            if (param.search.listCancelled != null)
                query = query.Where(a => a.IsApproved == !param.search.listCancelled);
            else
                query = query.Where(a => a.IsApproved == null);

            if (param.minDate != null)
                query = query.Where(a => a.Date.Date >= param.minDate);
            if (param.maxDate != null)
                query = query.Where(a => a.Date.Date <= param.maxDate);

            outQuery = query;
            paginatedQuery = query.Skip(param.start).Take(param.length);
        }
    }
}
