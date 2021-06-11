﻿using Data.Dtos;
using Core.Utility;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderManager
    {
        Task<Order> GetByIdAsync(int id);
        Task<List<Order>> Get(Expression<Func<Order, bool>> expression = null);
        Task<Result> Add(OrderAddDto dto);
        Task<Result> Update(OrderAddDto dto);
        Task<Result> Delete(Order entity);
    }
}
