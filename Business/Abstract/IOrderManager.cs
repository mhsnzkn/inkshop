using Data.Dtos;
using Core.Utility;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Utility.Datatables;

namespace Business.Abstract
{
    public interface IOrderManager
    {
        Task<Order> GetByIdAsync(int id);
        Task<List<Order>> Get(Expression<Func<Order, bool>> expression = null);
        Task<DataTableResult> GetOrderDataTable(DataTableParams param);
        Task<DataTableResult> GetReservationDataTable(DataTableParams param);
        Task<DataTableResult> GetTransferDataTable(DataTableParams param);
        Task<Result> AddOrder(OrderAddDto dto);
        Task<Result> ApproveOrder(int id);
        Task<Result> CancelOrder(int id, string message);
        Task<Result> UpdateOrder(OrderAddDto dto);
        Task<Result> PayReservation(int id);
        Task<Result> ApproveReservation(int id);
        Task<Result> CancelReservation(int id, string message);
        Task<Result> UpdateReservation(ReservationDto dto);
        Task<Result> AddTransfer(TransferDto dto);
        Task<Result> UpdateTransfer(TransferDto dto);
        Task<Result> Delete(Order entity);
    }
}
