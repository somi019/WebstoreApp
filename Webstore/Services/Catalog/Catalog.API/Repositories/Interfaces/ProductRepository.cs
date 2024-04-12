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

    }
}
