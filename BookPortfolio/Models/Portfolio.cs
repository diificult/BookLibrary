using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortfolio.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
         public string AppUserId {  get; set; }
        public int BookId { get; set; }    

        public int? Rating { get; set; }
        public string BookState {  get; set; }
        public string? Comment { get; set; }


        public AppUser AppUser { get; set; }
        public Book Book {  get; set; } 

    }
}
