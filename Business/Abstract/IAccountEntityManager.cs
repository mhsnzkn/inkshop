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
    public interface IAccountEntityManager
    {
        Task<AccountEntity> GetByIdAsync(int id);
        Task<List<AccountEntity>> Get(Expression<Func<AccountEntity, bool>> expression = null);
        Task<DataTableResult> GetForDataTable(DataTableParams param);
        Task<Result> Add(AccountEntity entity);
        Task<Result> Update(AccountEntity entity);
        Task<Result> Delete(AccountEntity entity);
        Task<List<SelectListItem>> GetForDropDown();
    }
}
