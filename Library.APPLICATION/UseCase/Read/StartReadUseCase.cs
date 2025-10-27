using Library.APPLICATION.Mapping;
using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Read
{
    public class StartReadUseCase
    {
        private readonly IReadInterface _read;
        private readonly ReadMap _readMap;

        public StartReadUseCase(IReadInterface read,ReadMap readMap)
        {
            _read = read;
            _readMap = readMap;
        }
        public async Task Execute(string UserId,Guid BookId)
        {
            var BookReadings = await _read.GetReadsByBook(BookId);
            if (BookReadings.Any())
            {
                throw new Exception("Book is already being read by another user");
            }
            await _read.StartRead(_readMap.StartRead(BookId, UserId));
            //await _read.StartRead(read);
        }
    }
}
