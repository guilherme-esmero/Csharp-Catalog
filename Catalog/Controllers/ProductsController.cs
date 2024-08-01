using Catalog.Context;
using Catalog.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductsController(AppDbContext context)
    {

        _context = context;

    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAsync()
    {

        //.AsNoTracking() para otimizar consultas onde ele define que a consulta não sera rastreada
        var products = await _context.products.AsNoTracking().ToListAsync();

        if (products.Count == 0)
        {
            return NotFound();
        }

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<List<Product>>> GetById(int id)
    {
        //.AsNoTracking() para otimizar consultas onde ele define que a consulta não sera rastreada
        var product = await _context.products.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Product product)
    {
        _context.products.Add(product);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task< ActionResult> PutAsync(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        await _context.SaveChangesAsync();

        return Ok(product);
    }
}
