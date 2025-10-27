using Library.APPLICATION.DTO.Book;
using Library.APPLICATION.UseCase.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "admin,staff")]
    public class BookController : ControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddBook(
            [FromBody] TransactionBookDTOs books,
            [FromServices] AddBookUseCase _addBook
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            await _addBook.Execute(books);
            return Ok("Book added successfully");
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
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            await _updateBook.Execute(Id, books);
            return Ok("Book updated successfully");
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
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            var result = await _getBook.GetById(Id);
            return Ok(new
            {
                Message = "Book retrieved successfully",
                Data = result
            });
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBookDTOs>>> GetBooks(
            [FromServices] GetBookUseCase _getBook
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            var result = await _getBook.GetAll();
            return Ok(new
            {
                Message = "Books retrieved successfully",
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
                    Message = "Invalid data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }
            await _deleteBook.Execute(Id);
            return Ok("Book deleted successfully");
        }
    }
}
