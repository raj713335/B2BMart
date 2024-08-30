using B2BMart.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2BMart.DataLayer.Repositories.Specifications 
{
    public class OrderByPaymentIntentWithItemsSpecification : BaseSpecifcation<Order>
    {
        public OrderByPaymentIntentWithItemsSpecification(string paymentIntentId)
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
