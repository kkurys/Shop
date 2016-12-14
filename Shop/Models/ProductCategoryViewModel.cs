using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ProductCategoryViewModel : BaseViewModel
    {
        public IList<Product> Product { get; set; }
        public IList<Category> Category { get; set; }
    }
}