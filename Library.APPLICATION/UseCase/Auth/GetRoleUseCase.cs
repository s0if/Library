using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Auth
{
    public class GetRoleUseCase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetRoleUseCase(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<string>> Execute(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null)
            {
                throw new Exception("user not found");
            }
            var role = await _userManager.GetRolesAsync(user);
            return role.ToList();
        }
    }
}
