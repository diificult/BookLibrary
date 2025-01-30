namespace BookPortfolio.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        //For the moment just get the first author? 
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string PublishDate { get; set; }
        public string? ISBN_10 { get; set; }
        public string? ISBN_13 { get; set; }
        public string language { get; set; } = string.Empty;

    }
}
