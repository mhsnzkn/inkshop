using Business.Abstract;
using Data.Entities;
using DataAccess.Abstract;
using Business.Generic;

namespace Business.Concrete
{
    public class OrderTypeManager : DefinitionManager<OrderType, IOrderTypeDal>, IOrderTypeManager
    {
        private readonly IOrderTypeDal orderTypeDal;

        public OrderTypeManager(IOrderTypeDal orderTypeDal) : base(orderTypeDal)
        {
            this.orderTypeDal = orderTypeDal;
        }
        
    }
}
