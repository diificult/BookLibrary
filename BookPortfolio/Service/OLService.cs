using BookPortfolio.Interfaces;
using BookPortfolio.Models;

namespace BookPortfolio.Service
{
    public class OLService : IOLService
    {

        private HttpClient _httpClient;
        private IConfiguration _configuration;
        public OLService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Book> FindBookByISBNSync(string ISBN_10)
        {
           try
            {
                var result = await _httpClient.GetAsync($"https://openlibrary.org/api/books?bibkeys=ISBN%3A{ISBN_10}&format=json");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                }
            }
        }
    }
}
