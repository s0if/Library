using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DOMAIN.MODEL
{
    public class BookPublisher
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Books Book { get; set; }

        public Guid PublisherId { get; set; }
        [ForeignKey(nameof(PublisherId))]
        public Publisher Publisher { get; set; }
    }
}
