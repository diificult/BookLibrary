using BookPortfolio.Interfaces;
using BookPortfolio.Mappers;
using BookPortfolio.Models;
using Newtonsoft.Json;

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

        public async Task<Book> FindBookByISBNSync(string ISBN)
        {
           try
            {
                var result = await _httpClient.GetAsync($"https://openlibrary.org/isbn/{ISBN}.json");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<OLBook>(content);
                    var book = tasks;
                    if (book != null)
                    {
                        //Get the author
                        var resultAuthor = await _httpClient.GetAsync($"https://openlibrary.org/{book.authors[0].key}.json");
                        if (result.IsSuccessStatusCode)
                        {
                            var contentAuthor = await resultAuthor.Content.ReadAsStringAsync();
                            var tasksAuthor = JsonConvert.DeserializeObject<OLAuthor>(contentAuthor);
                            var author = tasksAuthor;
                            if (author != null)
                            {
                                return book.ToBookFromOL(author);
                            }
                        }
                    }

                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.Source);
                return null;
            }
            return null;
        }
    }
}
