using System.Threading.Tasks;
using WPFSample.Domain;

namespace WPFSample.Repository.Contract
{
    public interface IProductImageRepository
    {
        ProductImage GetFirstImage(int idProduct);
    }
}