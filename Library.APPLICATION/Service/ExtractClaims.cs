using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Service
{
    public class ExtractClaims
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ExtractClaims(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string?> ExtractUserId(string Token)
        {

            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = TokenHandler.ReadJwtToken(Token);

            if (jwtToken.ValidTo < DateTime.UtcNow)
                return null;


            Claim userIdClaim = jwtToken.Claims.FirstOrDefault(type => type.Type == "userId");
            if (userIdClaim is null)
                return null;

            var user = await userManager.FindByIdAsync(userIdClaim.Value);
            if (user is not null)
                return user.Id;
            return null;
        }
    }

}
