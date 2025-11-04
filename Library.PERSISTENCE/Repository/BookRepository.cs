using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.PERSISTENCE.Repository
{
    public class BookRepository : GeneralsRepository<Books>, IBookInterface
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Books>> FilterAuthor(string Author)
        {
            return await _dbContext.Books
                .Where(b => b.Author.Contains(Author))
                .ToListAsync();
        }

        public async Task<IEnumerable<Books>> FilterDate(DateTime PublishedDate)
        {
            return await _dbContext.Books
                .Where(b => b.PublishedDate==PublishedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Books>> FilterISBN(string ISBN)
        {
            return await _dbContext.Books
                .Where(b => b.ISBN.Contains(ISBN))
                .ToListAsync();
        }

        public async Task<IEnumerable<Books>> FilterPrice(decimal LowestPrice, decimal HighestPrice)
        {
            return await _dbContext.Books
                .Where(b => b.Price >= LowestPrice && b.Price <= HighestPrice)
                .ToListAsync();
        }

        public async Task<IEnumerable<Books>> FilterTitle(string Title)
        {
            return await _dbContext.Books
                 .Where(b => b.Title.Contains(Title))
                 .ToListAsync();
        }
    }
}
