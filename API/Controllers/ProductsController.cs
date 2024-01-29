using Microsoft.AspNetCore.Mvc;

namespace Products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private ProductsContext _context;

    public ProductsController(ProductsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return _context.Products;
    }

    [HttpPost]
    public async Task Post(Product model)
    {
        await _context.Products.AddAsync(model);

        await _context.SaveChangesAsync();
    }
}

