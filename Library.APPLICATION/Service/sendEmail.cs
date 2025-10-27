using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Service
{
    public class sendEmail
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailSettings _email;

        public sendEmail(UserManager<ApplicationUser> userManager, EmailSettings email)
        {
            _userManager = userManager;
            _email = email;
        }
        public async Task ConfirmEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var _escapeToken = Uri.EscapeDataString(token);
            string TemplatePath = Path.Combine(AppContext.BaseDirectory, "Template", "ConfirmEmail.html");
            string emailBody = await File.ReadAllTextAsync(TemplatePath);
            emailBody = emailBody.Replace("{{UserName}}", user.UserName)
                   .Replace("{{ConfirmUrl}}", $"https://localhost:7085/Auth/ConfirmEmail?userId={user.Id}&token={_escapeToken}");
            await _email.SendEmailAsync(user.Email,
                "Confirm your email",
              emailBody);

        }
        public async Task ForgetPassword(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var _escapeToken = Uri.EscapeDataString(token);
            string TemplatePath = Path.Combine(AppContext.BaseDirectory, "Template", "ForgetPassword.html");
            string emailBody = await File.ReadAllTextAsync(TemplatePath);
            emailBody = emailBody.Replace("{{UserName}}", user.UserName)
                  .Replace("{{ResetUrl}}", $"https://localhost:7085/Auth/ResetPassword?userId={user.Id}&token={_escapeToken}");
            await _email.SendEmailAsync(user.Email,
             "Reset Your Password",
                emailBody);
        }
    }
}
