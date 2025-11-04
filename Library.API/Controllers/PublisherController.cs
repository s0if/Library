using Library.APPLICATION.DTO.Checkout;
using Library.APPLICATION.DTO.Publisher;
using Library.APPLICATION.UseCase.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Localization;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //

    public class PublisherController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public PublisherController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPublisher(
           [FromBody] TransactionCheckoutDTOs transaction,
           [FromServices] AddPublisherUseCase _addPublisher)
        {
            var UserId = User.Claims.FirstOrDefault(u=>u.Type.EndsWith("userId"))?.Value;
            if (transaction == null)
                return BadRequest(_localizer["Transaction data cannot be null."].Value);

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized(_localizer["User ID not found in token."].Value);
            var result = await _addPublisher.Execute(transaction, UserId,Request);
            return Ok(new
            {
                message = _localizer["Publisher added successfully."].Value,
                result
            });
        }
        [Authorize(Roles = "admin,staff")]
        
        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<GetPublisherDTOs>> GetById(Guid Id,
            [FromServices] GetPublisherUseCase _getPublisher)
        {
            var result=await _getPublisher.ExecuteById(Id);
            return Ok(result);
        }
        [Authorize(Roles = "admin,staff")]
        [HttpGet("Get")]
        public async Task<IActionResult> Get(
            [FromServices] GetPublisherUseCase _getPublisher)
        {
            var result=await _getPublisher.ExecuteAll();
            return Ok(result);
        }
        [Authorize(Roles = "admin,staff")]

        [HttpDelete("Delete/{PublisherId}")]
        public async Task<IActionResult> DeletePublisher(
            Guid PublisherId,
            [FromServices] DeletePublisherUseCase _deletePublisher)
        {
            await _deletePublisher.Execute(PublisherId);
            return Ok(_localizer["Publisher deleted successfully"].Value);
        }
    }                                   
}
