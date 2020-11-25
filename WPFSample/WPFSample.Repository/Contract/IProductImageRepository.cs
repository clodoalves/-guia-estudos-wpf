using System.Threading.Tasks;
using WPFSample.Domain;

namespace WPFSample.Repository.Contract
{
    public interface IProductImageRepository
    {
        Task<ProductImage> GetFirstImage(int idProduct);
    }
}