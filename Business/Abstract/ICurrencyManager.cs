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
    public interface ICurrencyManager
    {
        Task<Currency> GetByIdAsync(int id);
        Task<List<Currency>> Get(Expression<Func<Currency, bool>> expression = null);
        Task<Result> Add(Currency entity);
        Task<Result> Update(Currency entity);
        Task<Result> Delete(Currency entity);
        Task<List<SelectListItem>> GetForDropDown();
    }
}
