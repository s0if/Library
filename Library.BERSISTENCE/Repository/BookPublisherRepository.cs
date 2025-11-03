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
    public class BookPublisherRepository:GeneralsRepository<DOMAIN.MODEL.BookPublisher>,IBookPublisherInterface
    {
        private readonly ApplicationDbContext _dbContext;

        public BookPublisherRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookPublisher>> GetByPublisherId(Guid PublisherId)
        {
            return await _dbContext.BookPublishers
                .Where(x=>x.PublisherId==PublisherId)
                .ToListAsync();
             
        }
    }
}
