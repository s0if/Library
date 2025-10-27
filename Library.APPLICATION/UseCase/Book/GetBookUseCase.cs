using Library.APPLICATION.DTO.Book;
using Library.APPLICATION.Mapping;
using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Book
{
    public class GetBookUseCase
    {
        private readonly IBookInterface _book;
        private readonly BookMap _bookMap;

        public GetBookUseCase(IBookInterface book,BookMap bookMap)
        {
            _book = book;
            _bookMap = bookMap;
        }
        public async Task<GetBookDTOs> GetById(Guid Id)
        {
           var bookResult=await _book.GetById(Id);
            if(bookResult is null)
            {
                throw new Exception("Book not found");
            }
            var result =_bookMap.GetBookDTOs(bookResult);
            return result;
        }
        public async Task<IEnumerable<GetBookDTOs>> GetAll()
        {
            var bookResult=await _book.GetAll();
            if(bookResult is null)
            {
                throw new Exception("No books found");
            }
            var result=bookResult.Select(b=> _bookMap.GetBookDTOs(b)).ToList();
            return result;
        }
    }
}
