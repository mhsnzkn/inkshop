using Business.Abstract;
using Core.Utility.Datatables;
using Data.Entities;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Generic;

namespace Business.Concrete
{
    public class CurrencyManager : DefinitionManager<Currency, ICurrencyDal>, ICurrencyManager
    {
        private readonly ICurrencyDal currencyDal;

        public CurrencyManager(ICurrencyDal currencyDal) : base(currencyDal)
        {
            this.currencyDal = currencyDal;
        }
        
        public override async Task<List<SelectListItem>> GetForDropDown()
        {
            return await currencyDal.Get().OrderBy(a => a.Name).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();
        }

        public override async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = currencyDal.Get();
            // Filter
            if (!string.IsNullOrEmpty(param.search.value))
            {
                query = query.Where(a => a.Name.Contains(param.search.value) || a.ShortName.Contains(param.search.value) || a.Description.Contains(param.search.value));
            }
            if (param.length > 0)
            {
                query = query.Skip(param.start).Take(param.length);
            }
            var list = await query.OrderBy(a => a.Name).ToListAsync();

            // DataTableModel
            result.Data = list;
            result.Draw = param.draw;
            result.RecordsTotal = list.Count; 
            result.RecordsFiltered = result.RecordsTotal;

            return result;

        }
    }
}
