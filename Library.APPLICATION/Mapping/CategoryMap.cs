using Library.APPLICATION.DTO.Category;
using Library.DOMAIN.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.APPLICATION.Mapping
{
    public class CategoryMap
    {

        public GetCategoryDTOs ToCategoryDto(Category getCategory)
        {
            return new GetCategoryDTOs
            {
                Id = getCategory.Id,
                Name = getCategory.Name
            };
        }

        public Category ToCategory(TransactionCategoryDTOs getCategory)
        {
            return new Category
            {
                Name = getCategory.Name
            };
             
        }

    }
}
