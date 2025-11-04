using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DOMAIN.Interface
{
    public interface IBookInterface  :IGeneralInterface<Books>
    {
        public Task<IEnumerable<Books>> FilterTitle(string Title);
        public Task<IEnumerable<Books>> FilterAuthor(string Author);
        public Task<IEnumerable<Books>> FilterDate(DateTime PublishedDate);
        public Task<IEnumerable<Books>> FilterISBN(string ISBN);
        public Task<IEnumerable<Books>> FilterPrice(decimal LowestPrice,decimal HighestPrice);
    }
}
