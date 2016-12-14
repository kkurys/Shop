using System.Collections.Generic;

namespace Shop.Models
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}