using System.Collections.Generic;

namespace WPFSample.Domain
{
    public class Product : DomainBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }       
        public double Price { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
