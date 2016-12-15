using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Product : BaseViewModel
    {
        public int ProductID { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Krótki opis")]
        public string ShortDescription { get; set; }

        [Display(Name = "Cena netto")]
        public double? PriceNetto { get; set; }

        [Display(Name = "% VAT")]
        public int? VatPercent { get; set; }

        [Display(Name = "Aktualna cena")]
        public double? ActualPrice { get; set; }

        [Display(Name = "Producent")]
        public int ManufacturerID { get; set; }

        [Display(Name = "Kategoria")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [Display(Name = "Galeria zdjęć")]
        public virtual ICollection<ProductImage> Images { get; set; }

        public virtual ICollection<Copy> Copies { get; set; }
    }
}