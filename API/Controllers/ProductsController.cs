using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Nest;

namespace products;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductsContext _context;
    private readonly IElasticClient _elasticClient;

    public ProductsController(ProductsContext context, IElasticClient elasticClient)
    {
        _context = context;
        _elasticClient = elasticClient;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int page = 1, int size = 10)
    {
        return _context.Products
                        .AsNoTracking()
                        .OrderBy(x => x.Id)
                        .Skip((page - 1) * size)
                        .Take(size);
    }


    [HttpGet("search/{query}")]
    public async Task<IEnumerable<object>> Search(string query, int page = 1, int size = 10)
    {
        var response = await _elasticClient
                            .SearchAsync<ProductElastic>(x => x
                            .From((page - 1) * size)
                            .Size(size)
                            // .Query(q => q
                            //     .MultiMatch(m => m
                            //         .Fields(f => f
                            //             .Field("Name")
                            //             .Field("Description")
                            // )
                            // .Query(query)
                            // .Fuzziness(Fuzziness.Auto)))
                        );

        return response.Documents;
    }

    [HttpPost]
    public async Task Post(ProductModel model)
    {
        var newProduct = new Product(model.Name, model.Description, model.Price);

        await _context.Products.AddAsync(newProduct);

        await _context.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, ProductModel model)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            throw new ArgumentException("product not found");

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;

        await _context.SaveChangesAsync();
    }
}