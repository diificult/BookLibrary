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

                Id = book.BookId,
                Author = book.Book.Author,
                Title = book.Book.Title,
                PublishDate = book.Book.PublishDate,
                ISBN_10 = book.Book.ISBN_10,
                ISBN_13 = book.Book.ISBN_13,
                language = book.Book.language,
                coverIds = book.Book.coverIds,
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

            var allBooks = await books.ToListAsync();  // Fetch everything first
            foreach (var book in allBooks)
            {
                Console.WriteLine($"Book ID: {book.Id}, CoverIds: {string.Join(",", book.coverIds ?? new int[0])}");
            }
            var paginatedBooks = allBooks.Skip(skipNumber).Take(query.PageNumber);
            return paginatedBooks.ToList();
        }

        public async Task<Book?> GetByISBNAsync(string ISBN)
        {
            return await _context.books.FirstOrDefaultAsync(b => b.ISBN_10 == ISBN || b.ISBN_13 == ISBN);
        }
    }
}
