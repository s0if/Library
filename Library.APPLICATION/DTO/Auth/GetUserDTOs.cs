using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.DTO.Auth
{
    public class GetUserDTOs
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }
    }
}
