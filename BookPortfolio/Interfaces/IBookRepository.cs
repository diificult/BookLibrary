using BookPortfolio.Helpers;
using BookPortfolio.Models;

namespace BookPortfolio.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> CreateAsync(Book bookModel);
        Task<Book?> GetByIdAsync(int id);
        Task<List<Book>> GetAllAsync(BookQueryHelper query); 
    }
}
