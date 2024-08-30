using B2BMart.Models;

namespace B2BMart.API.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
