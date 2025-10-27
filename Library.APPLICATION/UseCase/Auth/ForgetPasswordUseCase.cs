using Library.APPLICATION.Service;
using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Auth
{
    public class ForgetPasswordUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly sendEmail _sendEmail;

        public ForgetPasswordUseCase(UserManager<ApplicationUser> userManager,sendEmail sendEmail)
        {
            _userManager = userManager;
            _sendEmail = sendEmail;
        }
        public async Task<string> ExecuteAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            _sendEmail.ForgetPassword(user);
            return "Password reset email sent. Please check your email.";
        }
    }
}
