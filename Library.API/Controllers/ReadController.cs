using Library.APPLICATION.UseCase.Read;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        //[Authorize(Roles = "admin,staff")]
        [HttpPost("start/{BookId}")]
        public async Task<IActionResult> StartRead(
            Guid BookId,
            [FromServices] StartReadUseCase _startRead
            )
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type.EndsWith("userId"))?.Value;
            if (string.IsNullOrEmpty(UserId))
            {
                throw new UnauthorizedAccessException("User ID not found in claims");
            }
            await _startRead.Execute(UserId, BookId);
            return Ok("Reading started successfully");
        }

        [Authorize(Roles = "admin,staff")]
        [HttpPost("start/{UserId}/{BookId}")]
        public async Task<IActionResult> StartReadSupervisor(
            string UserId,
           Guid BookId,
           [FromServices] StartReadUseCase _startRead
           )
        {
            if (string.IsNullOrEmpty(UserId) || BookId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User ID OR Book ID not found in claims");
            }
            await _startRead.Execute(UserId, BookId);
            return Ok("Reading started successfully");
        }

        [Authorize]
        [HttpPut("finish/{ReadId}")]
        public async Task<IActionResult> FinishRead(
            Guid ReadId,
            [FromServices] FinishReadUseCase _finishRead
            )
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type.EndsWith("userId"))?.Value;
            if (string.IsNullOrEmpty(UserId))
            {
                throw new UnauthorizedAccessException("User ID not found in claims");
            }
            await _finishRead.Execute(ReadId, UserId);
            return Ok("Reading finished successfully");
        }
        [Authorize(Roles = "admin,staff")]
        [HttpGet("ByBooks/{BookId}")]
        public async Task<IActionResult> GetReadsByBook(
            Guid BookId,
            [FromServices] GetAllReadsByBookUseCase _getReadsByBook
            )
        {
            var result = await _getReadsByBook.Execute(BookId);
            return Ok(new
            {
                Message = "Reads retrieved successfully",
                Data = result
            });
        }

        [Authorize(Roles = "admin,staff")]
        [HttpGet("ByUser/{UserId}")]
        public async Task<IActionResult> GetReadsByUser(
            string UserId,
            [FromServices] GetAllReadsByUserUseCase _getReadsByUser
            )
        {
            var result = await _getReadsByUser.Execute(UserId);
            return Ok(new
            {
                Message = "Reads retrieved successfully",
                Data = result
            });
        }
    }
}
