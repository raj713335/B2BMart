using B2BMart.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace B2BMart.DataLayer.Repositories
{
    public class ProductBrandRepository : GenericRepository<ProductBrand>, IProductBrandRepository
    {
        private readonly B2BMartContext _context;
        public ProductBrandRepository(B2BMartContext context) : base(context)
        {
            _context = context;
        }
    }

    public interface IProductBrandRepository : IGenericRepository<ProductBrand>
    {
    }


}
