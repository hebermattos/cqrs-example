using MassTransit;
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
    private readonly IBusControl _busControl;

    public ProductsController(ProductsContext context, IElasticClient elasticClient, IBusControl busControl)
    {
        _context = context;
        _elasticClient = elasticClient;
        _busControl = busControl;
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
    public async Task<IEnumerable<Product>> Search(string query, int page = 1, int size = 10)
    {
        var response = await _elasticClient
                            .SearchAsync<Product>(x => x
                            .From((page - 1) * size)
                            .Size(size)
                            .Query(q => q
                                .MultiMatch(m => m
                                    .Fields(f => f
                                        .Field("name")
                                        .Field("description")
                            )
                            .Query(query)
                            .Fuzziness(Fuzziness.Auto)
                        )));

        return response.Documents;
    }

    [HttpPost]
    public async Task Post(ProductModel model)
    {
        var newProduct = new Product{
            Description = model.Description,
            Name = model.Name,
            Price = model.Price
        };

        await _context.Products.AddAsync(newProduct);

        await _context.SaveChangesAsync();

        await _busControl.Publish(newProduct);
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

        model.Id = id;

        await _busControl.Publish(model);
    }
}