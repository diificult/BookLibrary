using BookPortfolio.Data;
using BookPortfolio.Helpers;
using BookPortfolio.Interfaces;
using BookPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace BookPortfolio.Repositorys
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(Book bookModel)
        {
            await _context.books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Book>> GetAllAsync(BookQueryHelper query)
        {
            var books = _context.books.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(query.Author))
            {
                books = books.Where(s => s.Author.Contains(query.Author));
            }
            if (!string.IsNullOrEmpty(query.Title))
            {
                books = books.Where(s=> s.Title.Contains(query.Title));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDescending ? books.OrderByDescending(s => s.Title) : books.OrderBy(s => s.Title);
                }
                if (query.SortBy.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDescending ? books.OrderByDescending(s => s.Author) : books.OrderBy(s => s.Author);
                }
                if (query.SortBy.Equals("Year", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDescending ? books.OrderByDescending(s => s.YearRelease) : books.OrderBy(s => s.YearRelease);
                }

            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            
            return await books.Skip(skipNumber).Take(query.PageNumber).ToListAsync();
        }

        public async Task<Book?> GetByISBNAsync(string ISBN_10)
        {
            return await _context.books.FirstOrDefaultAsync(b => b.ISBN_10 == ISBN_10);
        }
    }
}
