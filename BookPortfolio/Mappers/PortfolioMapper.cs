using BookPortfolio.Dtos.Portfolios;
using BookPortfolio.Models;
using Microsoft.Identity.Client;

namespace BookPortfolio.Mappers
{
    public static class PortfolioMapper
    {

        public static Portfolio ToPortfolioFromCreateDTO(this CreatePortfolioDto dto, string UserId, int bookId)
        {
            return new Portfolio
            {
                AppUserId = UserId,
                BookId = bookId,
                Rating = dto.Rating,
                BookState = dto.BookState,
                Comment = dto.Comment,
            };
        }
    }
}
