using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BERSISTENCE.Repository
{
    public class ReadRepository:IReadInterface
    {
        private readonly ApplicationDbContext _dbContext;

        public ReadRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task FinishRead(Reads read)
        {
             _dbContext.Reads.Update(read);
             await _dbContext.SaveChangesAsync();
        }

        public async Task<Reads> GetReadById(Guid ReadId)
        {
            return await _dbContext.Reads.AsNoTracking().
                FirstOrDefaultAsync(r => r.Id == ReadId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetReadsByBook(Guid BookId)
        {
           return await _dbContext.Reads.AsNoTracking().
                Where(r => r.BookId == BookId).
                Include(r=>r.User).
                Select(r=>r.User).
                ToListAsync();
        }

        public async Task<IEnumerable<Books>> GetReadsByUser(string userId)
        {
            return await _dbContext.Reads.AsNoTracking().
                Where(r => r.UserId == userId).
                Include(r => r.Book).
                Select(r => r.Book).
                ToListAsync();
        }

        public async Task StartRead(Reads read)
        {
           await _dbContext.Reads.AddAsync(read);
           await _dbContext.SaveChangesAsync();
        }
    }
}
