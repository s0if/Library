using Library.APPLICATION.DTO.Book;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Mapping
{
    public class BookMap
    {
        public Books ToBook(TransactionBookDTOs bookDTOs)
        {
            return new Books
            {
                Title = !string.IsNullOrEmpty( bookDTOs.Title)? bookDTOs.Title: "No Title",
                Author = !string.IsNullOrEmpty(bookDTOs.Author)?bookDTOs.Author:"Unknown Author",
                ISBN =!string.IsNullOrEmpty(bookDTOs.ISBN) ? bookDTOs.ISBN : "No ISBN",
                PublishedDate = bookDTOs.PublishedDate,
                CategoryId = bookDTOs.CategoryId  ,
                Price = bookDTOs.Price > 0 ? bookDTOs.Price : 0
            };
        }

        public GetBookDTOs GetBookDTOs(Books book)
        {
            return new GetBookDTOs
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                CategoryId = book.CategoryId,
                Price = book.Price
            };
        }
    }
}
