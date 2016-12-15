using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Data zatrudnienia")]
        public DateTime HireDate { get; set; }
        [Display(Name = "Rodzaj zatrudnienia")]
        public ContractType TypeOfContract { get; set; }
        [Display(Name = "Płaca")]
        public double Salary { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Sklep")]
        public int LocationID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ShopLocation Location { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
    public enum ContractType
    {
        [Description("Pełen etat")]
        FullTime,
        [Description("Pół etatu")]
        HalfTime
    }
}