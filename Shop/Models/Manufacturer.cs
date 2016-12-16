using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Nazwa kodowa")]
        public string Code { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}