using Business.Generic;
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
    public interface IAccountEntityManager : IDefinitionManager<AccountEntity>
    {
        Task<AccountEntityModel> GetModelByIdAsync(int id);
        Task<Result> Add(AccountEntityModel model);
        Task<Result> Update(AccountEntityModel model);
    }
}
