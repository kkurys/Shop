using System.Collections.Generic;

namespace Shop.Models
{
    public class CartViewModel : BaseViewModel
    {
        private double _totalSum = 0;
        private List<Product> _products;
        public CartViewModel(List<Product> cart)
        {
            if (cart != null)
            {
                _products = cart;
                foreach (Product p in _products)
                {
                    _totalSum += double.Parse(p.ActualPrice.ToString());
                }
            }
        }
        public List<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    return new List<Product>();
                }
                else
                {
                    return _products;
                }
            }
        }
        public double Total
        {
            get
            {
                return _totalSum;
            }
        }
    }
}