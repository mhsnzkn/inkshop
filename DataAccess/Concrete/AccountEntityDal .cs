using Core.DataAccess;
using Data;
using Data.Entities;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class AccountEntityDal : RepositoryBase<AccountEntity, AppDbContext>, IAccountEntityDal
    {
        public AccountEntityDal(AppDbContext context) : base(context)
        {

        }
    }
}
