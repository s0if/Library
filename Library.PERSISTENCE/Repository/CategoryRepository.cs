using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.PERSISTENCE.Repository
{
    public class CategoryRepository:GeneralsRepository<Category>, ICategoryInterface
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }   
    }
}
