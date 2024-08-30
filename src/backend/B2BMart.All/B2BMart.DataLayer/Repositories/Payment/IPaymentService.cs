using B2BMart.DataLayer.Models;
using B2BMart.DataLayer.Repositories.Basket;

namespace B2BMart.DataLayer.Repositories.Payment
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
