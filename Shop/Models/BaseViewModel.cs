using System.Collections.Generic;

namespace Shop.Models
{
    public class BaseViewModel
    {
        public ICollection<Category> Categories;
        public ICollection<CategoryWithSubcategories> MenuCategories;
    }
}