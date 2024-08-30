using B2BMart.DataLayer.Models;
using System;
using System.Linq.Expressions;

namespace B2BMart.DataLayer.Repositories.Specifications
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpecifcation<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(int userid) : base(o => o.UserId == userid)
        {
            AddInclude(o => o.OrderItems);
            //AddInclude(o => o.DeliveryId);
            AddOrderByDescending(o => o.CreatedAt);
        }

        public OrdersWithItemsAndOrderingSpecification(int userid, int orderId)
            : base(o => o.OrderId == orderId && o.UserId == userid)
        {
            AddInclude(o => o.OrderItems);
            //AddInclude(o => o.DeliveryId);
        }
    }
}
