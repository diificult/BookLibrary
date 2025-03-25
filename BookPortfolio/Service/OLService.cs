using BookPortfolio.Dtos.Book;
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

        //PUTTING ON PAUSE as I've changed plans, may delete if I decide to not go ahead. 
        /*
        public async Task<List<Book>> FindRelatedBooks(string ISBN)
        {
            List<Book> books = new List<Book>();
            try
            {
                var result = await _httpClient.GetAsync($"https://openlibrary.org/isbn/{ISBN}.json");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<OLBook>(content);
                    if (tasks != null)
                    {
                        books.Add(tasks.ToBookFromOL());
                        // Search for related books. 
                        var title = tasks.title;
                        title = title.Replace(' ', '+');
                        result = await _httpClient.GetAsync($"https://openlibrary.org/serach.json?title={title}&fields=editions");
                        if (result.IsSuccessStatusCode)
                        {
                             content = await result.Content.ReadAsStringAsync();

                        }
                    }
                    else
                    {
                        //Book not found.
                    }
                }
            }
        }
        */

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
                        string requestUrl = "";
                        if (book.authors == null)
                        {
                            var resultWorks = await _httpClient.GetAsync($"https://openlibrary.org/{book.works[0]}.json");
                            var tasksWorks = JsonConvert.DeserializeObject<OLWorks>(await resultWorks.Content.ReadAsStringAsync());
                            requestUrl = $"https://openlibrary.org/{tasksWorks.authors[0].author.key}.json";
                        }
                        else
                        {
                            requestUrl = $"https://openlibrary.org/{book.authors[0].key}.json";
                        }
                        var resultAuthor = await _httpClient.GetAsync(requestUrl);
                        //Get the author
                        if (result.IsSuccessStatusCode)
                        {
                            var contentAuthor = await resultAuthor.Content.ReadAsStringAsync();
                            var tasksAuthor = JsonConvert.DeserializeObject<OLAuthor>(contentAuthor);
                            var author = tasksAuthor;
                            if (author != null)
                            {
                                return book.ToBookFromOL(author);
                            }
                            else
                            {
                                //To Change to: Allow user to add author
                                return book.ToBookFromOL();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.Source);
                return null;
            }
            return null;
        }
    }
}
