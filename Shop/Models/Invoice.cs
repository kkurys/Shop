using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        [Display(Name = "Zamówienie")]
        public int OrderID { get; set; }

        [Display(Name = "Dane dostawy")]
        public int DeliveryDataID { get; set; }

        [Display(Name = "Data wystawienia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime Date { get; set; }

        [Display(Name = "Wystawił pracownik")]
        public int EmployeeID { get; set; }

        public virtual Order Order { get; set; }

        [Display(Name = "Dane dostawy")]
        public virtual DeliveryData DeliveryData { get; set; }

        public virtual Employee Employee { get; set; }

    }
}