using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.DTO.Checkout
{
    public class TransactionCheckoutDTOs
    {
        public IEnumerable<Guid> BookId { get; set; }=new HashSet<Guid>();
        public string PaymentMethod { get; set; }
    }
}
