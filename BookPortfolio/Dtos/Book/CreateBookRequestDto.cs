using System.ComponentModel.DataAnnotations;

namespace BookPortfolio.Dtos.Book
{
    public class CreateBookRequestDto
    {
        [Required]
        [MaxLength(64, ErrorMessage ="Author too long")]
        public string Author { get; set; } = string.Empty;
        [Required]
        [MaxLength(64, ErrorMessage ="Title too long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(64, ErrorMessage ="Release date too long")]
        public string PublishDate { get; set; }
        public string ISBN_10 { get; set; }
        public string ISBN_13 { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "language too long")]
        public string language { get; set; } = string.Empty;

    }
}
