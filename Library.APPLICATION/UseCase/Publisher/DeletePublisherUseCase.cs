using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Publisher
{
    public class DeletePublisherUseCase
    {
        private readonly IPublisherInterface _publisher;

        public DeletePublisherUseCase(IPublisherInterface publisher)
        {
            _publisher = publisher;
        }
        public async Task Execute(Guid publisherId)
        {
             await _publisher.Delete(publisherId);
        }

    }
}
