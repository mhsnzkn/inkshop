using Core.DataAccess;
using Data;
using Data.Constants;
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
    public class OrderPersonnelDal : RepositoryBase<OrderPersonnel, AppDbContext>, IOrderPersonnelDal
    {
        private readonly AppDbContext context;

        public OrderPersonnelDal(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// OrderPersonel tablosundakileri kontrol edip eklemek, duzenlemek veya silmek icin yapilan metot
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="personnelIds">1.Artist/ </param>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task Upsert(int orderId, int artistId, int infoMenId, int middleMenId)
        {
            if (orderId == 0)
                return;

            var list = await context.OrderPersonnel.Where(a => a.OrderId == orderId).ToListAsync();
            CheckOrderPersonel(list, artistId, orderId, OrderPersonnelJob.Artist);
            CheckOrderPersonel(list, infoMenId, orderId, OrderPersonnelJob.Info);
            CheckOrderPersonel(list, middleMenId, orderId, OrderPersonnelJob.Hanut);
            

        }

        private void CheckOrderPersonel(List<OrderPersonnel> list, int personnelId, int orderId, OrderPersonnelJob job)
        {
            var artist = list.Where(a => a.Job == job).FirstOrDefault();
            if (artist != null)
            {
                if (personnelId == 0)
                    context.Remove(artist);
                else
                    artist.PersonnelId = personnelId;
            }
            else
            {
                if (personnelId != 0)
                    context.Add(new OrderPersonnel { OrderId = orderId, PersonnelId = personnelId, Job = job });
            }
        }

        public void SetPersonnelPrice(OrderPersonnel orderPersonnel, Order order, decimal? price, ref decimal maxPrice)
        {
            if (orderPersonnel is not null)
            {
                var orderPrice = price ?? order.Price;
                orderPersonnel.Price = orderPrice * orderPersonnel.Personnel.Commission;
                maxPrice = maxPrice < orderPersonnel.Price ? orderPersonnel.Price : maxPrice;
                context.Update(orderPersonnel);
            }
        }

        public async Task AddInternalPersonnel(Order order, decimal orderPrice)
        {
            var internalPersonnel = await context.Personnel.Where(a => a.Category == PersonnelCategories.IcInfo).ToListAsync();
            foreach (var item in internalPersonnel)
            {
                var orderPersonnelToAdd = new OrderPersonnel()
                {
                    Job = OrderPersonnelJob.IcInfo,
                    Order = order,
                    Personnel = item,
                    Price = orderPrice * item.Commission
                };
                context.Add(orderPersonnelToAdd);
            }
        }

        public async Task AddFranchising(Order order)
        {
            if (order.OrderTypeId == OrderTypeId.Dovme.GetHashCode() || order.OrderTypeId == OrderTypeId.MakePiercing.GetHashCode())
            {
                var franchising = await context.Personnel.Where(a => a.Category == Data.Constants.PersonnelCategories.Franchising).FirstOrDefaultAsync();
                if (franchising is not null)
                {
                    var franchisingShare = new OrderPersonnel()
                    {
                        Order = order,
                        Personnel = franchising,
                        Price = order.Price * franchising.Commission
                    };
                    context.Add(franchisingShare);
                }
            }
        }
    }
}
