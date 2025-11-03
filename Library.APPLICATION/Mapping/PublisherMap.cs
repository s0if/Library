using Library.APPLICATION.DTO.Book;
using Library.APPLICATION.DTO.Checkout;
using Library.APPLICATION.DTO.Publisher;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Mapping
{
    public class PublisherMap
    {
        private readonly Service.TimeZone _timeZone;

        public PublisherMap(Library.APPLICATION.Service.TimeZone timeZone)
        {
            _timeZone = timeZone;
        }
        public async Task<(IEnumerable<BookPublisher> bookPublishers, IEnumerable<Publisher> publishers)>
     ToPublisher(TransactionCheckoutDTOs publisherDTOs, string UserId)
        {
            var publishers = new List<Publisher>();
            var bookPublishers = new List<BookPublisher>();

            foreach (var bookId in publisherDTOs.BookId)
            {
                var publisher = new Publisher
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    PublisherDate = _timeZone.GetPalestineTime()
                };

                var publisherBook = new BookPublisher
                {
                    BookId = bookId,
                    PublisherId = publisher.Id,
                    Publisher = publisher
                };

                publishers.Add(publisher);
                bookPublishers.Add(publisherBook);
            }

            return (bookPublishers, publishers);
        }

        
    }
}
