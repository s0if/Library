using Library.APPLICATION.DTO.Book;
using Library.APPLICATION.Mapping;
using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Book
{
    public class AddBookUseCase
    {
        private readonly IBookInterface _book;
        private readonly BookMap _bookMap;

        public AddBookUseCase(
            IBookInterface book,
            BookMap bookMap)
        {
            _book = book;
            _bookMap = bookMap;
        }

        public async Task Execute(TransactionBookDTOs books)
        {
           var result=await _bookMap.ToBook(books);

            if(result is null)
                throw new Exception("Mapping error");
              await _book.Add(result);
        }
    }
}
