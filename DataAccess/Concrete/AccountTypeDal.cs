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
    public class AccountTypeDal : RepositoryBase<AccountType, AppDbContext>, IAccountTypeDal
    {
        public AccountTypeDal(AppDbContext context) : base(context)
        {

        }
    }
}
