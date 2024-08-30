using B2BMart.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BMart.DataLayer.Repositories
{
    public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
    {
        private readonly B2BMartContext _context;
        public ProductTypeRepository(B2BMartContext context) : base(context)
        {
            _context = context;
        }
    }

    public interface IProductTypeRepository : IGenericRepository<ProductType>
    {
    }


}
