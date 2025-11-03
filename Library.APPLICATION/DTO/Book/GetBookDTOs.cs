using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.DTO.Book
{
    public class GetBookDTOs
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string FilePdf { get; set; }
    }
}
