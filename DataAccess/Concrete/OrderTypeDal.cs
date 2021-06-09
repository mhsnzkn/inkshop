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
    public class OrderTypeDal : RepositoryBase<OrderType, AppDbContext>, IOrderTypeDal
    {
        public OrderTypeDal(AppDbContext context) : base(context)
        {

        }
    }
}
