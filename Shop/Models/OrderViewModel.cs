using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class OrderViewModel : BaseViewModel
    {
        public Order Order { get; set; }

        private double _totalSum = 0;
        private Dictionary<Product, int> _products;
        public void SetProducts(Dictionary<Product, int> cart)
        {
            if (cart != null)
            {
                _products = cart;
                foreach (KeyValuePair<Product, int> pair in _products)
                {
                    double productActualPrice = double.Parse(pair.Key.ActualPrice.ToString());

                    _totalSum += productActualPrice * pair.Value;
                }
            }
        }
        public Dictionary<Product, int> Products
        {
            get
            {
                if (_products == null)
                {
                    return new Dictionary<Product, int>();
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