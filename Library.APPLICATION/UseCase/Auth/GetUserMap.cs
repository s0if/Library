using Library.APPLICATION.DTO.Auth;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Auth
{
    public class GetUserMap
    {
        public GetUserDTOs ToUser(ApplicationUser user)
        {
            return new GetUserDTOs
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                phoneNumber = user.PhoneNumber,
            };
        }
    }
}
