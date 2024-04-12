using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories.Interfaces
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

   


        public async Task<IEnumerable<Product>> GetProducts()
        {
            // treba da se importuje mongo, u njemu je metoda Find koji vraca IFindFluent koji nam odgovara
            return await _context.Products.Find(p=>true).ToListAsync();
            // spisak proizvoda ce pretvoriti u listu

        }


        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
            // FirstOrDefaultAsync vraca prvu ili null vrednost(proizvod ili ga nema) 

        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            return await _context.Products.Find(p => p.Category == categoryName).ToListAsync();

        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
           var updateResult = await _context.Products.ReplaceOneAsync(p=> p.Id == product.Id, product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _context.Products.DeleteOneAsync(p=>p.Id == id);
            return (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0);
        }
 
    }
}
