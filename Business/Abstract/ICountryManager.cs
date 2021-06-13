using Core.Utility;
using Core.Utility.Datatables;
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
    public interface ICountryManager
    {
        Task<Country> GetByIdAsync(int id);
        Task<List<Country>> Get(Expression<Func<Country, bool>> expression = null);
        Task<DataTableResult> GetForDataTable(DataTableParams param);
        Task<Result> Add(Country entity);
        Task<Result> Update(Country entity);
        Task<Result> Delete(Country entity);
        Task<List<SelectListItem>> GetForDropDown();
    }
}
