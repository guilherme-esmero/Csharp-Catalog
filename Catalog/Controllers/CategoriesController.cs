using Catalog.Context;
using Catalog.Entities;
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
    public async Task<ActionResult<List<Category>>> GetAsync()
    {
        try
        {
            //.AsNoTracking() para otimizar consultas onde ele define que a consulta não sera rastreada
            var categories = await _context.categories.Include(p => p.Products).AsNoTracking().ToListAsync();

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
    public async Task< ActionResult<List<Category>>> GetByIdAsync(int id)
    {
        try
        {
            //.AsNoTracking() para otimizar consultas onde ele define que a consulta não sera rastreada
            var category = await _context.categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

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
    public async Task<ActionResult> PostAsync(Category category)
    {
        _context.categories.Add(category);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutAsync(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

       await _context.SaveChangesAsync();

        return Ok(category);
    }
}
