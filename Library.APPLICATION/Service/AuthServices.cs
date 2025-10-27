using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Service
{
    public class AuthServices
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthServices(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var configurationsHey = configuration.GetSection("jwt")["secretkey"];
            var GetAudience= configuration.GetSection("jwt")["audience"];
            var GetIssuer= configuration.GetSection("jwt")["issuer"];
            var Authclaim = new List<Claim>()
          {
              new Claim("UserName",user.UserName) ,

              new Claim("userId",user.Id.ToString())
          };
            if (!string.IsNullOrEmpty(user.Email))
                  Authclaim.Add(new Claim("Email", user.Email));
            var userRole = await _userManager.GetRolesAsync(user);
            foreach (var role in userRole)
                Authclaim.Add(new Claim(ClaimTypes.Role, role));
            var keyAuth = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationsHey));
            var token = new JwtSecurityToken(
            //optinles
            audience: GetAudience,
            issuer: GetIssuer,

            //requierd
            claims: Authclaim,
            signingCredentials: new SigningCredentials(keyAuth,
           SecurityAlgorithms.HmacSha256Signature),
            //can change dateTime
            expires: DateTime.UtcNow.AddHours(1)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
