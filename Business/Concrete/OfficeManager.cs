using Business.Abstract;
using Core.Utility.Datatables;
using Data.Entities;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Business.Generic;

namespace Business.Concrete
{
    public class OfficeManager : DefinitionManager<Office, IOfficeDal>, IOfficeManager
    {
        private readonly IOfficeDal officeDal;
        public OfficeManager(IOfficeDal officeDal) : base(officeDal)
        {
            this.officeDal = officeDal;
        }
        

        public override async Task<DataTableResult> GetForDataTable(DataTableParams param)
        {
            var result = new DataTableResult();
            var query = officeDal.Get();
            // Filter
            if (!string.IsNullOrEmpty(param.search.value))
            {
                query = query.Where(a => a.Name.Contains(param.search.value) || a.City.Contains(param.search.value) || a.Description.Contains(param.search.value));
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
