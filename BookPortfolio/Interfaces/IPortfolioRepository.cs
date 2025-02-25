using BookPortfolio.Models;

namespace BookPortfolio.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Book>> GetUserPortfolio(AppUser user);
        Task<List<Portfolio>> GetUserPortfolioList(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio?> DeletePortfolioISBN(AppUser appUser, Book book);
        Task<bool> CheckIfPortfolioPublic(AppUser appUser);

        Task<Portfolio?> UpdateStateAsync(AppUser appUser, int bookId, string newState);
    }
}
