﻿using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IRepositoryBase<T> where T : class, IEntity, new()
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> expression = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task Save();
    }
}
