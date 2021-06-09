using Core.Utility;
using Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderTypeManager
    {
        Task<OrderType> GetByIdAsync(int id);
        Task<List<OrderType>> Get(Expression<Func<OrderType, bool>> expression = null);
        Task<Result> Add(OrderType entity);
        Task<Result> Update(OrderType entity);
        Task<Result> Delete(OrderType entity);
        Task<List<SelectListItem>> GetForDropDown();
    }
}
