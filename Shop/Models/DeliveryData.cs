namespace Shop.Models
{
    public class DeliveryData
    {
        public int DeliveryDataID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
        public string PostalCode { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}