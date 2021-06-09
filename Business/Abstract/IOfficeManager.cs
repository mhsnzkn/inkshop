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
    public interface IOfficeManager
    {
        Task<Office> GetByIdAsync(int id);
        Task<List<Office>> Get(Expression<Func<Office, bool>> expression = null);
        Task<Result> Add(Office entity);
        Task<Result> Update(Office entity);
        Task<Result> Delete(Office entity);
        Task<List<SelectListItem>> GetForDropDown();
    }
}
