using B2BMart.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BMart.DataLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly B2BMartContext _context;
        public ProductRepository(B2BMartContext context) : base(context)
        {
            _context = context;
        }

    }

    public interface IProductRepository : IGenericRepository<Product>
    {
    }


}
