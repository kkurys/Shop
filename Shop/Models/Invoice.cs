using System;

namespace Shop.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int OrderID { get; set; }
        public int DeliveryDataID { get; set; }
        public DateTime Date { get; set; }
        public int EmployeeID { get; set; }

        public virtual Order Order { get; set; }
        public virtual DeliveryData DeliveryData { get; set; }
        public virtual Employee Employee { get; set; }

    }
}