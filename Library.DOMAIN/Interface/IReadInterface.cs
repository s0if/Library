using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DOMAIN.Interface
{
    public interface IReadInterface
    {
        Task StartRead(Reads read);
        Task FinishRead(Reads read);
        Task<IEnumerable<Books>> GetReadsByUser(string userId);
        Task<IEnumerable<ApplicationUser>> GetReadsByBook(Guid BookId);
        Task<Reads> GetReadById(Guid ReadId);
    }
}
