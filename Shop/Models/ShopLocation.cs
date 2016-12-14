using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public class ShopLocation
    {
        //...
        public int ShopLocationID { get; set; }
        public DateTime OpeningHour { get; set; }
        public DateTime ClosingHour { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
        public string PostalCode { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}