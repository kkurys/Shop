using System.Collections.Generic;

namespace Shop.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public ICollection<News> News { get; set; }
    }
}