using B2BMart.DataLayer.Models;

namespace B2BMart.DataLayer.Repositories.OrderService
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int buyerEmail, int deliveryMethod, string basketId);
        Task<IReadOnlyList<Order>> GetOrderForUserAsync(int userid);
        Task<Order> GetOrderByIdAsync(int userid, int orderId);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}
