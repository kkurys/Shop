using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Order
    {
        [Display(Name = "ID")]
        public int OrderID { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [Display(Name = "Zapłacone")]
        public bool WasPaid { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Copy> Copies { get; set; }
    }
}