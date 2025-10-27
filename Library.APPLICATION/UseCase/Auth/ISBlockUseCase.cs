using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Auth
{
    public class ISBlockUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ISBlockUseCase(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> ExecuteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            return "User has been blocked successfully.";
        }  

        public async Task<string> UnblockAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            var result = await _userManager.SetLockoutEndDateAsync(user, null);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            return "User has been unblocked successfully.";
        }
    }
}
