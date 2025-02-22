﻿using Catalog.Core.Entities;
using Catalog.Core.Specs;


namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {

        Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams);

        Task<Product> GetProduct(string id);

        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<IEnumerable<Product>> GetProductByBrand(string name);

        Task<Product> CreateProduct(Product productType);

        Task<bool> UpdateProduct(Product productType);

        Task<bool> DeleteProduct(string id);
    
    }
}
