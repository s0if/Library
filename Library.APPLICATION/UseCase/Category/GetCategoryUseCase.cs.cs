using Library.APPLICATION.DTO.Category;
using Library.APPLICATION.Mapping;
using Library.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Category
{
    public class GetCategoryUseCase
    {
        private readonly ICategoryInterface _category;
        private readonly CategoryMap _categoryMap;

        public GetCategoryUseCase(
            ICategoryInterface category,
            CategoryMap categoryMap)
        {
            _category = category;
            _categoryMap = categoryMap;
        }
        public async Task<ActionResult<GetCategoryDTOs>> getById(Guid Id)
        {
           var categoryResult=await _category.GetById(Id);
            if(categoryResult is null)
            {
                throw new Exception("Category not found");
            }
            var categoryDTOs= _categoryMap.ToCategoryDto(categoryResult);

            return categoryDTOs;
        }
        public async Task<ActionResult<IEnumerable<GetCategoryDTOs>>> getAll()
        {
            var categoryResult=await _category.GetAll();
            if(categoryResult is null)
            {
                throw new Exception("No categories found");
            }
            var categoryDTOs=categoryResult.Select(c=> _categoryMap.ToCategoryDto(c)).ToList();
            return categoryDTOs;
        }
    }
}
