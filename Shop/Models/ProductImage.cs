namespace Shop.Models
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        public string FileName { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}