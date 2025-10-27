using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BERSISTENCE.Repository
{
    public class BookRepository : GeneralsRepository<Books>, IBookInterface
    {
        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        
    }
}
