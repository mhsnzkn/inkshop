using Core.Utility;
using Core.Utility.Datatables;
using Data.Dtos;
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
    public interface IAccountMovementManager
    {
        Task<AccountMovement> GetByIdAsync(int id);
        Task<AccountMovementModel> GetModelByIdAsync(int id);
        Task<List<AccountMovement>> Get(Expression<Func<AccountMovement, bool>> expression = null);
        Task<DataTableResult> GetForDataTable(AccountParamsDto param);
        Task<List<AccountMovementSumDto>> GetSummary(AccountParamsDto param);
        Task<Result> Add(AccountMovementModel model);
        Task<Result> Update(AccountMovementModel model);
        Task<Result> Delete(AccountMovement entity);
    }
}
