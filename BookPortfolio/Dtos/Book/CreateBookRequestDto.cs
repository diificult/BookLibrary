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
        [Range(0, 2024)]
        public int YearRelease { get; set; }
        [Required]
          
        public int ISBN_10 { get; set; }
        [Required]
        [MaxLength(32, ErrorMessage = "Genre too long")]
        public string genre { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "language too long")]
        public string language { get; set; } = string.Empty;

    }
}
