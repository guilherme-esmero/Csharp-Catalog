using Catalog.Context;
using Catalog.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        try
        {
            //.AsNoTracking() para otimizar consultas onde ele define que a consulta não sera rastreada
            var categories = _context.categories.Include(p => p.Products).AsNoTracking().ToList();

            if (categories.Count == 0)
            {
                return NotFound();
            }

            return Ok(categories);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha ao prosseguir com a requisição.");
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<List<Category>> GetById(int id)
    {
        try
        {
            //.AsNoTracking() para otimizar consultas onde ele define que a consulta não sera rastreada
            var category = _context.categories.AsNoTracking().FirstOrDefault(c => c.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha ao prosseguir com a requisição.");
        }
    }

    [HttpPost]
    public ActionResult Post(Category category)
    {
        _context.categories.Add(category);

        _context.SaveChanges();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        _context.SaveChanges();

        return Ok(category);
    }
}
