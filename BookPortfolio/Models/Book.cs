using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortfolio.Models
{
    [Table("Books")]
    public class Book
    {
        public int Id { get; set; }
        //For the moment just get the first author? 
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int YearRelease { get; set; }
        public string ISBN_10 {  get; set; }
        public string genre { get; set; } = string.Empty;
        public string language { get; set; } = string.Empty;

        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }
}
