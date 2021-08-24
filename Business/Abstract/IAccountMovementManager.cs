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
    public interface IAccountMovementManager
    {
        Task<AccountMovement> GetByIdAsync(int id);
        Task<List<AccountMovement>> Get(Expression<Func<AccountMovement, bool>> expression = null);
        Task<DataTableResult> GetForDataTable(DataTableParams param);
        Task<Result> Add(AccountMovement entity);
        Task<Result> Update(AccountMovement entity);
        Task<Result> Delete(AccountMovement entity);
    }
}
