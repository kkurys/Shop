using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
        public bool WasPaid { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Copy> Copies { get; set; }
    }
}