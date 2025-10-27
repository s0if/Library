using Library.APPLICATION.DTO.Auth;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Mapping
{
    public class RegisterMap
    {
        public ApplicationUser ToRegister(RegisterDTOs register)
        {
            var user = new ApplicationUser
            {
                UserName = register.UserName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
            };
            return user;
        }
    }
}
