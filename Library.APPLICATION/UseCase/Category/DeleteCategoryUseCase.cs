using Library.DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.UseCase.Category
{
    public class DeleteCategoryUseCase
    {
        private readonly ICategoryInterface _category;

        public DeleteCategoryUseCase(ICategoryInterface category)
        {
            _category = category;
        }
        public async Task Execute(Guid Id)
        {
            await _category.Delete(Id);
        }
    }
}
