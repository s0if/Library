using Library.APPLICATION.DTO.Book;
using Library.APPLICATION.Mapping;
using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Read
{
    public class GetAllReadsByUserUseCase
    {
        private readonly IReadInterface _read;
        private readonly BookMap _bookMap;

        public GetAllReadsByUserUseCase(IReadInterface read,BookMap bookMap)
        {
            _read = read;
            _bookMap = bookMap;
        }
        public async Task<IEnumerable<GetBookDTOs>> Execute(string UserId)
        {
            var books= await _read.GetReadsByUser(UserId);
            if (books is null || !books.Any())
            {
                throw new Exception("No reads found for this user");
            }
            return books.Select(b => _bookMap.GetBookDTOs(b));
        }
    }
}
