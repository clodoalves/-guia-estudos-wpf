using WPFSample.Domain;

namespace WPFSample.Repository.Contract
{
    public interface IProductRepository:IRepositoryBase<Product>
    {
        Product GetProductByTitle(string name);
    }
}
