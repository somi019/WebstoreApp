using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext() {
            // Ovaj kontekst je zaduzen za povezivanje sa bazom podataka
            var client = new MongoClient("mongodb://localhost:27017");
            // var isto kao auto u cpp, automatska dedukcija tipa
            var database = client.GetDatabase("CatalogDB");

            Products = database.GetCollection<Product>("Products");
            // povezali smo se na bazu podataka i uzeli jednu kolekciju podataka iz nje
            CatalogContextSeed.SeedData(Products);
            // ako zelis da imas neke podrazumevane proizvode, seedujes ih
            // sluzi nam cisto da bi imali nesto da dohvatimo radi testiranja API-ja

        }
        public IMongoCollection<Product> Products { get; }
    }
}
