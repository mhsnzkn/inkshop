using Core.Utility;
using Core.Utility.Datatables;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Generic
{
    public interface IDefinitionManager<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> Get(Expression<Func<T, bool>> expression = null);
        Task<DataTableResult> GetForDataTable(DataTableParams param);
        Task<Result> Add(T entity);
        Task<Result> Update(T entity);
        Task<Result> Delete(T entity);
        Task<List<SelectListItem>> GetForDropDown();
    }
}
