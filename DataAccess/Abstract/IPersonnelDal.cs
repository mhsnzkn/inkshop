using Core.DataAccess;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPersonnelDal : IRepositoryBase<Personnel>
    {
    }
}
