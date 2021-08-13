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
    public interface IVaultManager
    {
        Task<DataTableResult> GetIncomeDataTable(DataTableParams param);
        Task<DataTableResult> GetExpenseDataTable(DataTableParams param);
    }
}
