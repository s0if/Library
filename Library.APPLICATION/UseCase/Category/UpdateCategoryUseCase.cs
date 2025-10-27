using Library.APPLICATION.DTO.Category;
using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Category
{
    public class UpdateCategoryUseCase
    {
        private readonly ICategoryInterface _category;

        public UpdateCategoryUseCase(
            ICategoryInterface category)
        {
            _category = category;
        }
        public async Task Execute(Guid Id, TransactionCategoryDTOs category)
        {
           var categoryResult= await _category.GetById(Id);
            if (categoryResult is null)
             throw new Exception("Category not found");
            categoryResult.Name = category.Name;
            await _category.Update(categoryResult);
        }
    }
}
