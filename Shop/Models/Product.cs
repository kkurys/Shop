using System.Collections.Generic;

namespace Shop.Models
{
    public class Product : BaseViewModel
    {
        // ...
        public int ProductID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public double? PriceNetto { get; set; }
        public int? VatPercent { get; set; }
        public double? ActualPrice { get; set; }
        public int ManufacturerID { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<Copy> Copies { get; set; }
    }
}