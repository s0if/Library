using Library.APPLICATION.DTO.Book;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.DTO.Publisher
{
    public class GetPublisherDTOs
    {
        public Guid Id { get; set; }
        public DateTime PublisherDate { get; set; }
        public string UserId { get; set; }
        public Guid BookId { get; set; }
        public IEnumerable<GetBookDTOs> Books { get; set; }=new HashSet<GetBookDTOs>();

    }
}
