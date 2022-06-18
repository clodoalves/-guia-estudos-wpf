using System.Linq;
using WPFSample.Domain;
using WPFSample.Repository.Context;
using WPFSample.Repository.Contract;

namespace WPFSample.Repository.Implementation
{
    public class ProductImageRepository: RepositoryBase<ProductImage>, IProductImageRepository
    {
        public ProductImage GetFirstImage(int idProduct)
        {
            using (var db = new WPFSampleDbContext())
            {
                return db.ProductImages.Where(p => p.ProductId == idProduct).FirstOrDefault();
            }
        }
    }
}
