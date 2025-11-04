using Library.APPLICATION.DTO.Book;
using Library.APPLICATION.UseCase.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,staff")]
    public class BookController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public BookController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBook(
            [FromForm] TransactionBookDTOs books,
            [FromServices] AddBookUseCase _addBook
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = _localizer["Invalid data"].Value,
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            await _addBook.Execute(books);
            return Ok(_localizer["Book added successfully"].Value);
        }
        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> UpdateBook(
            Guid Id,
            [FromBody] TransactionBookDTOs books,
            [FromServices] UpdateBookUseCase _updateBook
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = _localizer["Invalid data"].Value,
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            await _updateBook.Execute(Id, books);
            return Ok(_localizer["Book updated successfully"].Value);
        }
        [AllowAnonymous]
        [HttpGet("ById/{Id}")]
        public async Task<ActionResult<GetBookDTOs>> GetBookById(
            Guid Id,
            [FromServices] GetBookUseCase _getBook
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = _localizer["Invalid data"].Value,
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            var result = await _getBook.GetById(Id);
            return Ok(new
            {
                Message = _localizer["Book retrieved successfully"].Value,
                Data = result
            });
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookDTOs>>> GetBooks(string? Title,
            string? Author ,
            DateTime? PublishedDate ,
            string? ISBN,
            decimal? LowestPrice,
            decimal? HighestPrice,
            [FromServices] GetBookUseCase _getBook
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = _localizer["Invalid data"].Value,
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            var result = await _getBook.GetAll(Title, Author, PublishedDate, ISBN, LowestPrice, HighestPrice);
            return Ok(new
            {
                Message = _localizer["Books retrieved successfully"].Value,
                Data = result
            });
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteBook(
            Guid Id,
            [FromServices] DeleteBookUseCase _deleteBook
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = _localizer["Invalid data"].Value,
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            await _deleteBook.Execute(Id);
            return Ok(_localizer["Book deleted successfully"].Value);
        }

    }
}
