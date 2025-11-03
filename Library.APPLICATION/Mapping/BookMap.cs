using Library.APPLICATION.DTO.Book;
using Library.APPLICATION.Service;
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
        private readonly UploadFile _uploadFile;

        public BookMap(UploadFile uploadFile)
        {
            _uploadFile = uploadFile;
        }
        public async Task<Books> ToBook(TransactionBookDTOs bookDTOs)
        {
            var url = await _uploadFile.UploadPdfAsync(bookDTOs.FilePdf);
            return new Books
            {
                Title = !string.IsNullOrEmpty( bookDTOs.Title)? bookDTOs.Title: "No Title",
                Author = !string.IsNullOrEmpty(bookDTOs.Author)?bookDTOs.Author:"Unknown Author",
                ISBN =!string.IsNullOrEmpty(bookDTOs.ISBN) ? bookDTOs.ISBN : "No ISBN",
                PublishedDate = bookDTOs.PublishedDate,
                CategoryId = bookDTOs.CategoryId  ,
                Price = bookDTOs.Price > 0 ? bookDTOs.Price : 0  ,
                Description = !string.IsNullOrEmpty(bookDTOs.Description) ? bookDTOs.Description : "No Description"   ,
                BookFile = url

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
                Price = book.Price ,
                Description = book.Description   ,
                FilePdf= book.BookFile

            };
        }
    }
}
