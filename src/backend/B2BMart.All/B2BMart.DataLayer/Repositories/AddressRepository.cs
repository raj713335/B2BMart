using B2BMart.DataLayer.Models;


namespace B2BMart.DataLayer.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly B2BMartContext _context;
        public AddressRepository(B2BMartContext context) : base(context)
        {
            _context = context;
        }
    }

    public interface IAddressRepository : IGenericRepository<Address>
    {
    }
}
