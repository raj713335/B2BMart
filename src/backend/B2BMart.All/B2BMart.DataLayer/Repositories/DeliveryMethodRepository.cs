using B2BMart.DataLayer.Models;


namespace B2BMart.DataLayer.Repositories
{
    public class DeliveryMethodRepository : GenericRepository<DeliveryMethod>, IDeliveryMethodRepository
    {
        private readonly B2BMartContext _context;
        public DeliveryMethodRepository(B2BMartContext context) : base(context)
        {
            _context = context;
        }
    }

    public interface IDeliveryMethodRepository : IGenericRepository<DeliveryMethod>
    {
    }
}
