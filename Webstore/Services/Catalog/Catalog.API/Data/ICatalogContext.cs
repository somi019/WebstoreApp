using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface ICatalogContext
    {
        // Ovaj interfejs definise obrazac inverzije zavisnosti (od njega zavise drugi servisi)
        IMongoCollection<Product> Products { get; }
    }
}
