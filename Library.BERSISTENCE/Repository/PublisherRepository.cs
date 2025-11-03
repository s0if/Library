using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BERSISTENCE.Repository
{
    public class PublisherRepository:GeneralsRepository<DOMAIN.MODEL.Publisher>,IPublisherInterface
    {
        public PublisherRepository(ApplicationDbContext dbContext): base(dbContext) { }
    }
}
