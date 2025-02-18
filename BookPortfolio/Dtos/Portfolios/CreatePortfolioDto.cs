using System.ComponentModel.DataAnnotations;

namespace BookPortfolio.Dtos.Portfolios
{
    public class CreatePortfolioDto
    {
        public string ISBN { get; set; }
        [Range(1, 5)]
        public int? Rating { get; set; }
        //To be updated possibly for enums?
        [Required]
        [MaxLength(10)]
        public string BookState { get; set; }
        [MaxLength(1024)]   
        public string? Comment { get; set; }

    }
}
