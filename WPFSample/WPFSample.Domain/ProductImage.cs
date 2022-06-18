
namespace WPFSample.Domain
{
    public class ProductImage : DomainBase
    {
        public string Path { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
