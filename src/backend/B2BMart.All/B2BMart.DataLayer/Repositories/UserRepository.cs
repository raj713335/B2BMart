using B2BMart.DataLayer.Models;


namespace B2BMart.DataLayer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly B2BMartContext _context;
        public UserRepository(B2BMartContext context) : base(context)
        {
            _context = context;
        }
    }

    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
