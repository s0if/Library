using Library.APPLICATION.DTO.Category;
using Library.APPLICATION.UseCase.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Library.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,staff")]
    public class CategoryController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoryController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpPost("Add")]
        public  async Task<IActionResult> AddCategory(
            [FromBody] TransactionCategoryDTOs category,
            [FromServices] AddCategoryUseCase _addCategory
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
            await _addCategory.Execute(category);
            return Ok(_localizer["Category added successfully"].Value);
        }
        [AllowAnonymous]
        [HttpGet("ById/{Id}")]
        public async Task<IActionResult> GetCategoryById( 
            Guid Id,
            [FromServices] GetCategoryUseCase _getCategory
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
            var result = await _getCategory.getById(Id);
            return Ok(new
            {
                Message = _localizer["Category retrieved successfully"].Value,
                Data = result.Value
            });
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCategory([FromServices] GetCategoryUseCase _getCategory)
        {
            
            var result = await _getCategory.getAll();
            return Ok(new
            {
                Message = _localizer["Category retrieved successfully"].Value,
                Data = result.Value
            });
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> UpdateCategory(
            Guid Id,
            [FromBody] TransactionCategoryDTOs category,
            [FromServices] UpdateCategoryUseCase updateCategory
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
            await updateCategory.Execute(Id, category);
            return Ok(_localizer["Category updated successfully"].Value);
        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteCategory(
            Guid Id,
            [FromServices] DeleteCategoryUseCase deleteCategory
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
            await deleteCategory.Execute(Id);
            return Ok(_localizer["Category deleted successfully"].Value);
        }
    }
}
