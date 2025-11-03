using Library.APPLICATION.DTO.Book;
using Library.APPLICATION.DTO.Publisher;
using Library.APPLICATION.Mapping;
using Library.DOMAIN.Interface;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Publisher
{
    public class GetPublisherUseCase
    {
        private readonly IPublisherInterface _publisher;
        private readonly IBookPublisherInterface _bookPublisher;
        private readonly IBookInterface _book;
        private readonly BookMap _bookMap;

        public GetPublisherUseCase(IPublisherInterface publisher,IBookPublisherInterface bookPublisher,IBookInterface book,BookMap bookMap)
        {
            _publisher = publisher;
            _bookPublisher = bookPublisher;
            _book = book;
            _bookMap = bookMap;
        }
        private async Task<IEnumerable<GetBookDTOs>> GetById(Guid Id)
        {
            var result = await _bookPublisher.GetByPublisherId(Id);
            if (result is null)
                throw new Exception("Book Not Found");
            List<GetBookDTOs> books = new List<GetBookDTOs>();
            foreach (var item in result)
            {
                var book = _bookMap.GetBookDTOs(await _book.GetById(item.BookId));
                books.Add(book);
            }
            return books;
        }
        public async Task<IEnumerable<GetPublisherDTOs>> ExecuteAll()
        {
            var result = await _publisher.GetAll();
            if (result is null)
                throw new Exception("No Publish Found");
            
            
            var dtoList = new List<GetPublisherDTOs>();
            foreach (var x in result)
            {
                var book= await GetById(x.Id);
                dtoList.Add(new GetPublisherDTOs
                {
                    Id = x.Id,
                    PublisherDate = x.PublisherDate,
                    UserId = x.UserId,
                    Books = book.ToList()
                });
            }
            return dtoList;
        }
        public async Task<GetPublisherDTOs> ExecuteById(Guid Id)
        {
            var result = await _publisher.GetById(Id);
            if (result is null)
                throw new Exception(" Publish Not Found");
            var book = await GetById(result.Id);
            return  new GetPublisherDTOs
            {
                Books = book.ToList(),
                Id = result.Id,
                PublisherDate = result.PublisherDate,
                UserId= result.UserId,
            };
        }
    }
}
