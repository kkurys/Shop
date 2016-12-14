using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class CategoryWithSubcategories
    {
        // działaj kurwa na tym jebanym gitcie

        public Category Category { get; set; }
        public List<Category> Subcategories { get; set; }

        public CategoryWithSubcategories()
        {

        }

        public CategoryWithSubcategories(Category argCategory)
        {
            Category = argCategory;
            Subcategories = new List<Category>();
        }

        static Predicate<CategoryWithSubcategories> FindFather(int? fatherId)
        {
            return delegate (CategoryWithSubcategories cat)
            {
                return cat.Category.CategoryID == fatherId;
            };
        }

        public List<CategoryWithSubcategories> getAllCategories(ApplicationDbContext db)
        {
            List<CategoryWithSubcategories> menu = new List<CategoryWithSubcategories>();

            foreach (Category cat in db.Categories.ToList())
            {
                if (cat.BaseCategoryID == null)
                {
                    menu.Add(new CategoryWithSubcategories(cat));
                }
                else
                {
                    menu.Find(FindFather(cat.BaseCategoryID)).Subcategories.Add(cat);
                }
            }
            return menu;
        }
    }
}