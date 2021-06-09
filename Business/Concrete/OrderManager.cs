using Business.Abstract;
using Core.Utility;
using Data.Entities;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderDal orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            this.orderDal = orderDal;
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

        public async Task<Result> Add(Order entity)
        {
            var result = new Result();
            try
            {
                entity.CrtDate = DateTime.Now;
                orderDal.Add(entity);
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

        public async Task<Result> Update(Order entity)
        {
            var result = new Result();
            try
            {
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
    }
}
