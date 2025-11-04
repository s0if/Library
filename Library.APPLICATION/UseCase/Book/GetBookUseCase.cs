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
        public async Task<IEnumerable<GetBookDTOs>> GetAll(
            string? Title=null,
            string? Author = null,
            DateTime? PublishedDate = null, 
            string? ISBN = null,
            decimal? LowestPrice = null,
            decimal? HighestPrice = null)
        {
            var bookResult=new List<GetBookDTOs>();
            if (!string.IsNullOrEmpty(Title))
            {
                var result= await _book.FilterTitle(Title);
                if(result is not null)
                    bookResult.AddRange(result.Select(b=>_bookMap.GetBookDTOs(b)).ToList());
            }
            if (!string.IsNullOrEmpty(Author))
            {
                var result= await _book.FilterAuthor(Author);
                if (result is not null)
                    bookResult.AddRange(result.Select(b=>_bookMap.GetBookDTOs(b)).ToList());
            }
            if (PublishedDate is not null)
            {
                var result= await _book.FilterDate(PublishedDate.Value);
                if (result is not null)
                    bookResult.AddRange(result.Select(b=>_bookMap.GetBookDTOs(b)).ToList());
            }
            if (!string.IsNullOrEmpty(ISBN))
            {
                var result= await _book.FilterISBN(ISBN);
                if (result is not null)
                    bookResult.AddRange(result.Select(b=>_bookMap.GetBookDTOs(b)).ToList());
            }
            if (LowestPrice is not null&& HighestPrice is not null)
            {
                var result= await _book.FilterPrice(LowestPrice.Value, HighestPrice.Value);
                if (result is not null)
                    bookResult.AddRange(result.Select(b=>_bookMap.GetBookDTOs(b)).ToList());
            }
            if (!bookResult.Any())
            {
                var result = await _book.GetAll();
                if(result is null)
                {
                    throw new Exception("No books found");
                }
                 bookResult = result.Select(b=> _bookMap.GetBookDTOs(b)).ToList();
            }
                return bookResult.GroupBy(b=>b.Id).Select(g=>g.First()).ToList();


        }
    }
}
