using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class DeliveryData
    {
        [Display(Name = "Dane dostawy")]
        public int DeliveryDataID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        public string EMail { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Miasto")]
        public string City { get; set; }
        [Display(Name = "Ulica i nr domu")]
        public string FullAddress { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}