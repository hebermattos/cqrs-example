using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
    public IEnumerable<Product> Get(int page = 1, int size = 10)
    {
        return _context.Products.OrderBy(x => x.Id)
                        .Skip((page - 1) * size)
                        .Take(size);
    }

    [HttpPost]
    public async Task Post(Product model)
    {
        await _context.Products.AddAsync(model);

        await _context.SaveChangesAsync();
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
    }
}

