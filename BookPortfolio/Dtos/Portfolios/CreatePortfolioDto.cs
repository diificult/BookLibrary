using System.ComponentModel.DataAnnotations;

namespace BookPortfolio.Dtos.Portfolios
{
    public class CreatePortfolioDto
    {
        public string ISBN { get; set; }
        [Required]
        [Range(1, 5)]
        public int? Rating { get; set; }
        //To be updated
        [Required]
        [MaxLength(10)]
        public string BookState { get; set; }

        public string? Comment { get; set; }

    }
}
