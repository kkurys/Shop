using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        [Display(Name = "Zamówienie")]
        public int OrderID { get; set; }
        public int DeliveryDataID { get; set; }
        [Display(Name = "Data wystawienia")]
        public DateTime Date { get; set; }
        [Display(Name = "Wystawił pracownik")]
        public int EmployeeID { get; set; }

        public virtual Order Order { get; set; }
        public virtual DeliveryData DeliveryData { get; set; }
        public virtual Employee Employee { get; set; }

    }
}