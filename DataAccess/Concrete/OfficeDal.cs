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
    public class OfficeDal : RepositoryBase<Office, AppDbContext>, IOfficeDal
    {
        public OfficeDal(AppDbContext context) : base(context)
        {

        }
    }
}
