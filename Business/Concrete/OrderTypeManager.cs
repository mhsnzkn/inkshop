using Business.Abstract;
using Core.Utility;
using Data.Entities;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderTypeManager : IOrderTypeManager
    {
        private readonly IOrderTypeDal orderTypeDal;

        public OrderTypeManager(IOrderTypeDal orderTypeDal)
        {
            this.orderTypeDal = orderTypeDal;
        }
        
        public async Task<List<OrderType>> Get(System.Linq.Expressions.Expression<Func<OrderType, bool>> expression = null)
        {
            return await orderTypeDal.Get(expression).ToListAsync();
        }

        public async Task<OrderType> GetByIdAsync(int id)
        {
            OrderType result = null;
            try
            {
                result = await orderTypeDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(OrderType entity)
        {
            var result = new Result();
            try
            {
                entity.CrtDate = DateTime.Now;
                orderTypeDal.Add(entity);
                await orderTypeDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> Delete(OrderType entity)
        {
            var result = new Result();
            try
            {
                orderTypeDal.Delete(entity);
                await orderTypeDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> Update(OrderType entity)
        {
            var result = new Result();
            try
            {
                entity.UptDate = DateTime.Now;
                orderTypeDal.Update(entity);
                await orderTypeDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<List<SelectListItem>> GetForDropDown()
        {
            return await orderTypeDal.Get().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();
        }
    }
}
