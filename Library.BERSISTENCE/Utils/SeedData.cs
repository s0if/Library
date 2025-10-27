using Library.BERSISTENCE;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DOMAIN.MODEL;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Library.APPLICATION.Utils
{
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task DataSeedings()
        {
            if ( _dbContext.Database.GetPendingMigrations().Any())
            {
                _dbContext.Database.Migrate();
            }
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                await _roleManager.CreateAsync(new IdentityRole("staff"));
                await _roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (!_userManager.Users.Any())
            {
               

                var admin=new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    Email = "admin@saif.com",
                    EmailConfirmed = true
                };
                var staff=new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "staff",
                    Email = "staff@saif.com",
                    EmailConfirmed = true
                };
                var user=new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user",
                    Email = "user@saif.com",
                    EmailConfirmed = true
                };
                
                
                await _userManager.CreateAsync(admin, "@Admin12");
                await _userManager.CreateAsync(staff, "@Staff12");
                await _userManager.CreateAsync(user, "@User12");


                await _userManager.AddToRoleAsync(admin, "admin");
                await _userManager.AddToRoleAsync(staff, "staff");
                await _userManager.AddToRoleAsync(user, "user");
               

             
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
