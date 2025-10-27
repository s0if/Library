using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DOMAIN.Interface
{
    public interface IGeneralInterface <T> where T : class
    {
        public Task Add(T entity);
        public Task Delete(Guid Id);
        public Task Update(T entity);
        public Task<T> GetById(Guid Id);
        public Task<IEnumerable<T>> GetAll();

    }
}
