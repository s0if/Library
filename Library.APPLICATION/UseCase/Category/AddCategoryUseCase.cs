using Library.APPLICATION.DTO.Category;
using Library.APPLICATION.Mapping;
using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Category
{
    public class AddCategoryUseCase
    {
        private readonly ICategoryInterface _category;
        private readonly CategoryMap _categoryMap;

        public AddCategoryUseCase(
            ICategoryInterface category,
            CategoryMap categoryMap)
        {
            _category = category;
            _categoryMap = categoryMap;
        }
        public async Task Execute(TransactionCategoryDTOs category)
        {
            var categoryModel= _categoryMap.ToCategory(category);
            await _category.Add( categoryModel);
        }
    }
}
