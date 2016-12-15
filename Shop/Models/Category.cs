using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Display(Name = "Nazwa kategorii")]
        public string Name { get; set; }
        [ForeignKey("BaseCategory")]
        [Display(Name = "Kategoria bazowa")]
        public int? BaseCategoryID { get; set; }
        public virtual Category BaseCategory { get; set; }
        public virtual ICollection<Category> ChildrenCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}