using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class ShopLocation
    {
        //...
        public int ShopLocationID { get; set; }
        [Display(Name = "Godzina otwarcia")]
        public DateTime OpeningHour { get; set; }
        [Display(Name = "Godzina zamknięcia")]
        public DateTime ClosingHour { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string EMail { get; set; }
        [Display(Name = "Miasto")]
        public string City { get; set; }
        [Display(Name = "Ulica i numer lokalu")]
        public string FullAddress { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}