using System.ComponentModel.DataAnnotations.Schema;

namespace BookPortfolio.Models
{
    [Table("Portfolios")]
    public class Porfolio
    {
         public string AppUserId {  get; set; }
        public int BookId { get; set; }    

        public AppUser AppUser { get; set; }
        public Book Book {  get; set; } 

    }
}
