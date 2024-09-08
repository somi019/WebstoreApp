using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration) {
            // Ovaj kontekst je zaduzen za povezivanje sa bazom podataka
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            // var isto kao auto u cpp, automatska dedukcija tipa
            var database = client.GetDatabase("CatalogDB");

            Products = database.GetCollection<Product>("Products");
            // povezali smo se na bazu podataka i uzeli jednu kolekciju podataka iz nje
            CatalogContextSeed.SeedData(Products);
            // ako zelis da imas neke podrazumevane proizvode, seedujes ih
            // sluzi nam cisto da bi imali nesto da dohvatimo radi testiranja API-ja

        }
        public IMongoCollection<Product> Products { get; }
        // ovo je promenljiva Products
    }
}
