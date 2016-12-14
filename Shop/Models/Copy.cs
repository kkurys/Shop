namespace Shop.Models
{
    public class Copy
    {
        public int CopyID { get; set; }
        public int ProductID { get; set; }
        public string SerialNumber { get; set; }
        public bool WasSold { get; set; }
        public int LocationID { get; set; }
        public virtual ShopLocation Location { get; set; }
    }
}