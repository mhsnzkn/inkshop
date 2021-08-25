using Core.Utility;
using Core.Utility.Datatables;
using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAccountTypeManager
    {
        Task<AccountType> GetByIdAsync(int id);
        Task<AccountTypeModel> GetModelByIdAsync(int id);
        Task<List<AccountType>> Get(Expression<Func<AccountType, bool>> expression = null);
        Task<DataTableResult> GetForDataTable(DataTableParams param);
        Task<Result> Add(AccountType entity);
        Task<Result> Update(AccountType entity);
        Task<Result> Delete(AccountType entity);
        Task<List<SelectListItem>> GetForDropDown();
    }
}
