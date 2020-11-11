﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Domain;

namespace WPFSample.Repository.Contract
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);

        Task<IList<Product>> ListAllProducts();
    }
}
