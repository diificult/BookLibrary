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

        public async Task<bool> CheckIfPortfolioPublic(AppUser appUser)
        {
            var Portfolios = await _context.Users.Include(u => u.Portfolios).FirstOrDefaultAsync(u => u.Id == appUser.Id);
            if (Portfolios == null) return false;
            return Portfolios.isPortfolioPublic;
            
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolioISBN(AppUser appUser, Book book)
        {
            var portfolioModel = await _context.portfolios.FirstOrDefaultAsync(u => u.AppUserId == appUser.Id 
                && (u.Book.ISBN_10.ToLower() == book.ISBN_10.ToLower() 
                || u.Book.ISBN_13.ToLower() == book.ISBN_13.ToLower()));
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
        public async Task<List<Portfolio>> GetUserPortfolioList(AppUser user)
        {
            return await _context.portfolios.Where(u => u.AppUserId == user.Id).Include(p => p.Book).ToListAsync();
        }

        public async Task<List<Portfolio>> GetUserReviews(string userId)
        {
            return await _context.portfolios.Where(u => u.AppUserId == userId).Include(p => p.Book).Where(p => p.Rating != null).ToListAsync();
        }

        public async Task<Portfolio?> UpdateStateAsync(AppUser appUser, int bookId, string newState)
        {
            var existingPortfolio = await _context.portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.BookId == bookId);
            if (existingPortfolio == null) return null;

            existingPortfolio.BookState = newState;

            await _context.SaveChangesAsync();
            return existingPortfolio;
        }
    }
}
