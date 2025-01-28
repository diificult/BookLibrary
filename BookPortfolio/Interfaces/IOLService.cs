using BookPortfolio.Models;

namespace BookPortfolio.Interfaces
{
    public interface IOLService 
    {
        Task<Book> FindBookByISBNSync(string ISBN_10);
    }
}
