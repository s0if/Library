using Library.APPLICATION.DTO.Auth;
using Library.APPLICATION.UseCase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterUseCase _registerUseCase;
        private readonly ConfirmEmailUseCase _confirmEmail;
        private readonly ForgetPasswordUseCase _forgetPassword;
        private readonly resetPasswordUseCase _resetPassword;
        private readonly ISBlockUseCase _iSBlock;
        private readonly IStringLocalizer< SharedResource> _localizer;

        public AuthController(LoginUseCase loginUseCase,
            RegisterUseCase registerUseCase,
            ConfirmEmailUseCase confirmEmail,
            ForgetPasswordUseCase forgetPassword,
            resetPasswordUseCase resetPassword,
            ISBlockUseCase iSBlock,
            IStringLocalizer<SharedResource> localizer)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _confirmEmail = confirmEmail;
            _forgetPassword = forgetPassword;
            _resetPassword = resetPassword;
            _iSBlock = iSBlock;
            _localizer = localizer;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTOs registerDTOs)
        {
            var result = await _registerUseCase.ExecuteAsync(registerDTOs,Request);
            return Ok(_localizer[result].Value);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTOs loginDTOs)
        {
            var token = await _loginUseCase.ExecuteAsync(loginDTOs, Request);
            if (!string.IsNullOrEmpty(token))
            {
               
                return Ok(new
                {
                    Message = _localizer["Login successful"].Value,
                    Token = token,
                });
            }
            throw new Exception(_localizer["Invalid login"].Value);
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var result = await _confirmEmail.ExecuteAsync(userId, token);
            return Ok(_localizer[result].Value);
        }
        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTOs changePasswordDTOs)
        {
            var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(_localizer["User ID not found in token."].Value);
            }
            var changePasswordUseCase = HttpContext.RequestServices.GetService(typeof(ChangePasswordUseCase)) as ChangePasswordUseCase;
            if (changePasswordUseCase == null)
            {
                throw new Exception(_localizer["ChangePassword use case not available."].Value);
            }
            var result = await changePasswordUseCase.ExecuteAsync(userId, changePasswordDTOs.CurrentPassword, changePasswordDTOs.NewPassword);
            return Ok(_localizer[result].Value);
        }
        [HttpPost("GenerateRestPassword")]
        public async Task<IActionResult> RestPassword(string email)
        {
            var result = await _forgetPassword.ExecuteAsync(email);
            return Ok(_localizer[result].Value);
        }
        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string userId, [FromQuery] string token)
        {
            string html = await _resetPassword.getRestPassword(userId, token);
            return Content(html, "text/html");
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTOs resetPassword)
        {

            var result = await _resetPassword.ExecuteAsync(resetPassword.UserId, resetPassword.Token, resetPassword.NewPassword);
            return Ok(_localizer[result].Value);
        }

        [Authorize(Roles ="admin")]
        [HttpPost("ISBlock/{ID}")]
        public async Task<IActionResult> ISBlock(string ID)
        {
             var result=await _iSBlock.ExecuteAsync(ID);
            return Ok(_localizer[result].Value);
        }
        [Authorize(Roles = "admin")]
        [HttpPost("UNBlock/{ID}")]
        public async Task<IActionResult> UNBlock(string ID)
        {
            var result = await _iSBlock.UnblockAsync(ID);
            return Ok(_localizer[result].Value);
        }
    }
}
