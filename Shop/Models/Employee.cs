using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Shop.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string UserID { get; set; }
        public DateTime HireDate { get; set; }
        public ContractType TypeOfContract { get; set; }
        public double Salary { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
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