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
    public interface IPersonnelManager
    {
        Task<Personnel> GetByIdAsync(int id);
        Task<List<Personnel>> Get(Expression<Func<Personnel, bool>> expression = null);
        Task<DataTableResult> GetForDataTable(DataTableParams param);
        Task<Result> Add(Personnel entity);
        Task<Result> Update(Personnel entity);
        Task<Result> Delete(Personnel entity);
        Task<List<SelectListItem>> GetForDropDown();
        Task<List<SelectListItem>> GetArtistPersonnelForDropDown();
        Task<List<SelectListItem>> GetMiddlePersonnelForDropDown();
        Task<List<SelectListItem>> GetInfoPersonnelForDropDown();
    }
}
