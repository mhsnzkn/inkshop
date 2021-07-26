using Core.DataAccess;
using Data.Constants;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderPersonnelDal : IRepositoryBase<OrderPersonnel>
    {
        Task Upsert(int orderId, int artistId, int infoMenId, int middleMenId);
    }
}
