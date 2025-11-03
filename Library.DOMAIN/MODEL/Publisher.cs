using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DOMAIN.MODEL
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public DateTime PublisherDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public IEnumerable<Books> Book { get; set; }=new HashSet<Books>();

    }
}
