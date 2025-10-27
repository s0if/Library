using Library.APPLICATION.UseCase.Auth;
using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.APPLICATION.Mapping
{
    public class ReadMap
    {
        private readonly Service.TimeZone _timeZone;

        public ReadMap(Library.APPLICATION.Service.TimeZone timeZone)
        {
            _timeZone = timeZone;
        }
        public Reads StartRead(Guid bookId, string userId)
        {
            return new Reads
            {
                BookId = bookId,
                UserId = userId,
                ReadDate = _timeZone.GetPalestineTime(),
                FinishDate = null
            };
        }
        public async Task<Reads> FinishRead(Reads reads)
        {
             reads.FinishDate = _timeZone.GetPalestineTime();
            return reads;

        }
    }
}
