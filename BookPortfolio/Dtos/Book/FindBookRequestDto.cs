using System.ComponentModel.DataAnnotations;

namespace BookPortfolio.Dtos.Book
{
    public class FindBookRequestDto
    {
        [Required]
        [MaxLength(14, ErrorMessage = "ISBN is too long")]
        public string ISBN { get; set; } = String.Empty;
    }
}
