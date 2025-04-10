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



            var books = _context.books.Select(book => new Book
            {

                Id = book.Id,
                Author = book.Author,
                Title = book.Title,
                PublishDate = book.PublishDate,
                ISBN_10 = book.ISBN_10,
                ISBN_13 = book.ISBN_13,
                language = book.language,
                coverIds = book.coverIds,
            }).AsQueryable();



            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                books = books.Where(s => s.Author.ToUpper().Contains(query.SearchTerm.ToUpper()) || s.Title.ToUpper().Contains(query.SearchTerm.ToUpper()));
                Console.WriteLine("Books is" + books.Count());
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

            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            //  return await books.Skip(skipNumber).Take(query.PageNumber).ToListAsync();
            return await books.ToListAsync();
        }

        public async Task<Book?> GetByISBNAsync(string ISBN)
        {
            return await _context.books.FirstOrDefaultAsync(b => b.ISBN_10 == ISBN || b.ISBN_13 == ISBN);
        }
    }
}
