using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;

namespace WPFSample.Service.Contract
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
    }
}
