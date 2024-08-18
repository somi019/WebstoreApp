using Catalog.API.Data;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    // da bi se omogucio swagger, svi API zahtevi treba da budu kontrolisani sa APIControllerom
    // Route kaze, NA KOM URLU JA PRIHVATAM ZAHTEV (za ovo sto sam napisao "api/v1/Catalog" je ruta)
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        //////// OVDE NIJE U REDU :
        ////////    kad imamo neki projekat koji ima SQLServer i sef kaze sad mi treba mongo
        ////////    ovde ne treba da menjamo samo klase za mongo (umesto njih da mecemo klase SQLSrw)
        ////////    kontroler sad zavisi direktno od mongo-a
        ////////    treba da menjamo implementaciju kontrolera da bi promenili bazu, to nam ne treba
        ////////    ako ja ocu da menjam bazu, kontrolera treba da bude briga za to
        ////////    treba da napravimo apstrakciju za find
        //ICatalogContext _context;
        //public CatalogController(ICatalogContext context)
        //{ 
        //    _context = context;
        //} 
        //public GetProducts()
        //{
        //    _context.Products.FindSync<Product>();
        //}

        private readonly IProductRepository _repository;

        public CatalogController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // ovaj metod implementira GET zahtev na /api/v1/Catalog
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            // ako jedna funckija poziva drugu asinhronu funckiju i ona mora da bude async
            // a tamo gde dobijas podatke treba da imas await
            var products = await _repository.GetProducts();
            return Ok(products);
            // ako pronadjemo proizvod onda cemo da vratimo Ok sa tim proizvodom
            // ako ne nadjemo vratimo null ili NotFound
            // sve iz ove klase ControllerBase se odnosi na statusne kodove

        }
        [HttpGet("{id:length(24)}",Name = "GetProduct")] // /api/v1/Catalog/{id} -> /api/v1/Catalog/12345 
        [ProducesResponseType(typeof(Product),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);
            if(product == null)
            {
                return NotFound(null);
            }
            return Ok(product);
        }
        [Route("[action]/{category}")]
        [HttpGet] // /api/v1/Catalog/GetProductByCategory/{category}
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            var product = await _repository.GetProductsByCategory(category);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        { 
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
        }
            
    }
}
