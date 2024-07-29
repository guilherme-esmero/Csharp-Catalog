using Catalog.Context;
using Catalog.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoriesController(AppDbContext context)
    {

        _context = context;

    }

    [HttpGet]
    public ActionResult<List<Category>> Get()
    {
        var categories = _context.categories.ToList();

        if (categories.Count == 0)
        {
            return NotFound();
        }

        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public ActionResult<List<Category>> GetById(int id)
    {
        var category = _context.categories.FirstOrDefault(c=> c.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        _context.categories.Add(category);

        _context.SaveChanges();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id,Category category)
    {
        if(id != category.Id)
        {
            return BadRequest();
        }

        _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        _context.SaveChanges();

        return Ok(category);
    }
}
