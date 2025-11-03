using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.DTO.Checkout
{
    public class GetCheckoutDTOs
    {
        public bool IsCheckedOut { get; set; }
        public string Message { get; set; }
        public string UrlStribe { get; set; }
    }
}
