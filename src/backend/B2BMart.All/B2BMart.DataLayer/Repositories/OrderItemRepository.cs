using B2BMart.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BMart.DataLayer.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly B2BMartContext _context;
        public OrderItemRepository(B2BMartContext context) : base(context)
        {
            _context = context;
        }

    }

    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
    }


}
