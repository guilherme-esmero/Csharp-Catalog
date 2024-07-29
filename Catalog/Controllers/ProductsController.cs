using Catalog.Context;
using Catalog.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<List<Product>> Get()
    {
        var products = _context.products.ToList();

        if (products.Count == 0)
        {
            return NotFound();
        }

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public ActionResult<List<Product>> GetById(int id)
    {
        var product = _context.products.FirstOrDefault(c => c.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        _context.products.Add(product);

        _context.SaveChanges();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        _context.SaveChanges();

        return Ok(product);
    }
}
