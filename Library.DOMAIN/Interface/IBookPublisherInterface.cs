using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DOMAIN.Interface
{
    public interface IBookPublisherInterface:IGeneralInterface<BookPublisher>
    {
       public Task<IEnumerable<BookPublisher>> GetByPublisherId(Guid PublisherId);
    }
}
