using Library.APPLICATION.DTO.Auth;
using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Auth
{
    public class resetPasswordUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public resetPasswordUseCase(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> ExecuteAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var _UnEscapeToken = Uri.UnescapeDataString(token);
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            var result = await _userManager.ResetPasswordAsync(user, _UnEscapeToken, newPassword);
            if (result.Succeeded)
            {
                return "Password reset successfully.";
            }
            else
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

       public async Task<string> getRestPassword(string UserId,string Token)
        {
            string TemplatePath = Path.Combine(AppContext.BaseDirectory, "Template", "ResetPassword.html");
            string html = await File.ReadAllTextAsync(TemplatePath);
            html = html.Replace("{{userId}}", UserId)
                       .Replace("{{token}}", Token);
            return html;
        }
    }
}
