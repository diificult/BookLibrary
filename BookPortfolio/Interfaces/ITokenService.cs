using BookPortfolio.Models;

namespace BookPortfolio.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
