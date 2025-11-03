using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.DTO.Publisher
{
    public class TransactionPublisherDTOs
    {
        
        public IEnumerable<Guid> BookId { get; set; }=new HashSet<Guid>();
        public string PaymentMethod { get; set; }
    }
}
