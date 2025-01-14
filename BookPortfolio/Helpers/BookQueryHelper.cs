namespace BookPortfolio.Helpers
{
    public class BookQueryHelper
    {
        public string? Author { get; set; } = null;
        public string? Title { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public bool IsDescending { get; set; } = false;
        public int PageNumber {  get; set; } = 1;
        public int PageSize { get; set;} = 20; 

    }
}
