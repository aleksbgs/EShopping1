using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;


namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _context = catalogContext;
        }


        public async Task<Product> CreateProduct(Product productType)
        {
            await _context.Products.InsertOneAsync(productType);
            return productType;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => id, id);

            DeleteResult deleteResult = await _context
                .Products
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _context
               .Brands
               .Find(b => true)
               .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _context
                 .Types
                 .Find(t => true)
                 .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context
                .Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, name);

            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _context
                .Products
                .Find(filter)
                .ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                 .Products
                 .Find(p => true)
                 .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product productType)
        {
            var updateResult = await _context
                 .Products
                 .ReplaceOneAsync(p => p.Id == productType.Id, productType);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }
    }
}
