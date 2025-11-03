using Library.APPLICATION.DTO.Auth;
using Library.APPLICATION.Mapping;
using Library.APPLICATION.Service;
using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Auth
{
    public class RegisterUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RegisterMap _registerMap;
        private readonly EmailSettings _email;
        private readonly sendEmail _sendEmail;

        public RegisterUseCase(UserManager<ApplicationUser> userManager,
            RegisterMap registerMap,
            EmailSettings email,
            sendEmail sendEmail)
        {
            _userManager = userManager;
            _registerMap = registerMap;
            _email = email;
            _sendEmail = sendEmail;
        }
        public async Task<string> ExecuteAsync(RegisterDTOs registerDTOs,HttpRequest request)
        {
            var user = _registerMap.ToRegister(registerDTOs); 
            var result =  await _userManager.CreateAsync(user, registerDTOs.Password);  

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                await _sendEmail.ConfirmEmail(user, request);
                return "User registered successfully. Check you email to confirm";
            }
            else
            {
               throw new Exception( string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }
       
    }
}
