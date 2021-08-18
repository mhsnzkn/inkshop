using Core.DataAccess;
using Data;
using Data.Entities;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class PersonnelDal : RepositoryBase<Personnel, AppDbContext>, IPersonnelDal
    {
        private readonly AppDbContext context;

        public PersonnelDal(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Personnel> GetFranchising()
        {
            return await context.Personnel.Where(a => a.Category == Data.Constants.PersonnelCategories.Franchising).FirstOrDefaultAsync();
        }
    }
}
