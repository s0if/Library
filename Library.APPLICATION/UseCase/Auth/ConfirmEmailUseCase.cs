using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Auth
{
    public class ConfirmEmailUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmailUseCase(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> ExecuteAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email confirmed successfully.";
            }
            else
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
