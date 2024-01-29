using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private ProductsContext _context;
    private IElasticClient _elasticClient;

    public ProductsController(ProductsContext context, IElasticClient elasticClient)
    {
        _context = context;
        _elasticClient = elasticClient;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int page = 1, int size = 10)
    {
        return _context.Products.OrderBy(x => x.Id)
                        .Skip((page - 1) * size)
                        .Take(size);
    }


    [HttpGet("search/{query}")]
    public async Task<IEnumerable<Product>> Search(string query, int page = 1, int size = 10)
    {
        var response = await _elasticClient.SearchAsync<Product>(x => x
            .From((page - 1) * size)
            .Size(size)
            .Query(q => q
                .MultiMatch(m => m                    
                    .Fields(f => f
                        .Field("name")
                        .Field("descirption")
            )
            .Query(query)
        )));

        return response.Documents;
    }

    [HttpPost]
    public async Task Post(Product model)
    {
        var newProduct = await _context.Products.AddAsync(model);

        await _context.SaveChangesAsync();

        model.Id = newProduct.Entity.Id;

        await _elasticClient.IndexDocumentAsync(model);
    }

    [HttpPut("{id}")]
    public async Task Put(int id, Product model)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            throw new ArgumentException("product not found");

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;

        await _context.SaveChangesAsync();

        await _elasticClient.UpdateAsync<Product>(id, u => u
                            .Doc(product));

    }
}

