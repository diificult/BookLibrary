using BookPortfolio.Data;
using BookPortfolio.Interfaces;
using BookPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace BookPortfolio.Repositorys
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;

        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolioISBN(AppUser appUser, string ISBN_10)
        {
            var portfolioModel = await _context.portfolios.FirstOrDefaultAsync(u => u.AppUserId == appUser.Id && u.Book.ISBN_10.ToLower() == ISBN_10.ToLower());
            if (portfolioModel != null) { return null; }
            _context.portfolios.Remove(portfolioModel); 
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Book>> GetUserPortfolio(AppUser user)
        {
            return await _context.portfolios.Where(u => u.AppUserId == user.Id).Select(book => new Book
            {
                Id = book.BookId,
                Author = book.Book.Author,
                Title = book.Book.Title,
                PublishDate = book.Book.PublishDate,
                ISBN_10 = book.Book.ISBN_10,
                ISBN_13 = book.Book.ISBN_13,
                language = book.Book.language,
                coverIds = book.Book.coverIds,
            }).ToListAsync();
        }
    }
}
