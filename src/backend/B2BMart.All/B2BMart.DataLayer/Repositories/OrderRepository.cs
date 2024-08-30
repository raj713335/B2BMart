using B2BMart.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BMart.DataLayer.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly B2BMartContext _context;
        public OrderRepository(B2BMartContext context) : base(context)
        {
            _context = context;
        }

    }

    public interface IOrderRepository : IGenericRepository<Order>
    {
    }


}
