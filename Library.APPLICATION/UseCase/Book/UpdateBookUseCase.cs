using Library.APPLICATION.DTO.Book;
using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Book
{
    public class UpdateBookUseCase
    {
        private readonly IBookInterface _book;

        public UpdateBookUseCase(IBookInterface book)
        {
            _book = book;
        }
        public async Task Execute(Guid Id, TransactionBookDTOs book)
        {
           var bookResult= await _book.GetById(Id);
            if (bookResult is null)
             throw new Exception("Book not found");
            bookResult.Title = book.Title;
            bookResult.Author = book.Author;
            bookResult.ISBN = book.ISBN;
            bookResult.PublishedDate = book.PublishedDate;
            bookResult.CategoryId = book.CategoryId;
            await _book.Update(bookResult);
        }
    }
}
