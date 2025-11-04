using Library.DOMAIN.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.PERSISTENCE.Repository
{
    public class GeneralsRepository<T> : IGeneralInterface<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GeneralsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await  _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
             _dbContext.Set<T>().Remove(await GetById(Id));
             await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAll()
        {
           var GetAll=await _dbContext.Set<T>().ToListAsync();
                return GetAll;
        }

        public async Task<T> GetById(Guid Id)
        {
            var getById=await _dbContext.Set<T>().FindAsync(Id);
                return getById;

        }

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
