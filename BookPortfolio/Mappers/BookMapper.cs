using BookPortfolio.Dtos.Book;
using BookPortfolio.Models;

namespace BookPortfolio.Mappers
{
    public static class BookMapper
    {
        public static Book ToBookFromCreateDto (this CreateBookRequestDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                YearRelease = bookDto.YearRelease,
                ISBN_10 = bookDto.ISBN_10,
                genre = bookDto.genre,
                language = bookDto.language,
            };
        }

       public static BookDto ToBookDto(this Book bookModel)
        {
            return new BookDto
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Author = bookModel.Author,
                YearRelease = bookModel.YearRelease,
                ISBN_10 = bookModel.ISBN_10,
                genre = bookModel.genre,
                language = bookModel.language,
            };
        }

    }
}
