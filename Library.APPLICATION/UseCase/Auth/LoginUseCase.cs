using Library.APPLICATION.DTO.Auth;
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
    public class LoginUseCase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AuthServices _authServices;
        private readonly sendEmail _sendEmail;

        public LoginUseCase(SignInManager<ApplicationUser> signInManager,AuthServices authServices,sendEmail sendEmail)
        {
            _signInManager = signInManager;
            _authServices = authServices;
            _sendEmail = sendEmail;
        }
        public async Task<string> ExecuteAsync(LoginDTOs loginDTOs)
        {

            if ((string.IsNullOrEmpty(loginDTOs.Email)&& (string.IsNullOrEmpty(loginDTOs.UserName)))|| string.IsNullOrEmpty(loginDTOs.Password))
            {
                throw new ArgumentException("Username or Email and password must be provided.");
            }
            var user=new ApplicationUser();
            if ((string.IsNullOrEmpty(loginDTOs.UserName)))
            {
                 user = await _signInManager.UserManager.FindByEmailAsync(loginDTOs.Email);
            }
            else
            {
                 user = await _signInManager.UserManager.FindByNameAsync(loginDTOs.UserName);
            }
            if (user is null)
                throw new Exception("User not found.");
            if(!await _signInManager.UserManager.IsEmailConfirmedAsync(user))
            {
                await _sendEmail.ConfirmEmail(user);
                throw new Exception("Email is not confirmed. Please check your email, confirm it, and try again.");

            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDTOs.Password, isPersistent: true, lockoutOnFailure: true);
            if(!result.Succeeded)
            {
                var errors = new List<string>();
                if (result.IsLockedOut) errors.Add("User is locked out.");
                if (result.IsNotAllowed) errors.Add("User is not allowed to sign in.");
                if (result.RequiresTwoFactor) errors.Add("Two-factor authentication is required.");
                if (!result.Succeeded && errors.Count == 0) errors.Add("Invalid login attempt.");
                throw new Exception(string.Join(", ", errors));
            }
             return await tokenGenerate(user);
        }
        private async Task<string> tokenGenerate(ApplicationUser user)
        {
              var token =await  _authServices.CreateTokenAsync(user);
            return token;
        }
    }
}
