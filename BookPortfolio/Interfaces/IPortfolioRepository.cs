using BookPortfolio.Models;

namespace BookPortfolio.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Book>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolioISBN(AppUser appUser, string ISBN);
    }
}
