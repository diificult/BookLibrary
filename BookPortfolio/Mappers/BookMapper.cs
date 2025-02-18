using BookPortfolio.Dtos.Book;
using BookPortfolio.Models;

namespace BookPortfolio.Mappers
{
    public static class BookMapper
    {
        public static Book ToBookFromCreateDto(this CreateBookRequestDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                PublishDate = bookDto.PublishDate,
                ISBN_10 = bookDto.ISBN_10,
                ISBN_13 = bookDto.ISBN_13,
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
                PublishDate = bookModel.PublishDate,
                ISBN_10 = bookModel.ISBN_10,
                ISBN_13 = bookModel.ISBN_13,
                language = bookModel.language,
            };
        }

        public static Book ToBookFromOL(this OLBook olBookModel, OLAuthor olAuthorModel)
        {

            return new Book
            {
                Author = olAuthorModel.name,
                Title = olBookModel.title,
                PublishDate = olBookModel.publish_date,
                ISBN_10 = olBookModel.isbn_10?[0],
                ISBN_13 = olBookModel.isbn_13?[0],
                language = "en",
                coverIds = olBookModel.covers,
            };


        }
        public static Book ToBookFromOL(this OLBook olBookModel)
        {

            return new Book
            {
                Author = "n/a",
                Title = olBookModel.title,
                PublishDate = olBookModel.publish_date,
                ISBN_10 = olBookModel.isbn_10?[0],
                ISBN_13 = olBookModel.isbn_13?[0],
                language = "en",
                coverIds = olBookModel.covers,
            };


        }

    }
}
