using BackendProject.App.Data;
using BackendProject.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public IngredientesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/ingredientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetIngredientes()
    {
        var ingredientes = await _context.Ingredientes
            .Include(i => i.ReceitaIngredientes)
            .OrderBy(i => i.Nome)
            .Select(i => new
            {
                i.Id,
                i.Nome,
                QuantidadeReceitas = i.ReceitaIngredientes.Count
            })
            .ToListAsync();

        return Ok(ingredientes);
    }

    // GET: api/ingredientes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetIngrediente(int id)
    {
        var ingrediente = await _context.Ingredientes
            .Include(i => i.ReceitaIngredientes)
                .ThenInclude(ri => ri.Receita)
            .Where(i => i.Id == id)
            .Select(i => new
            {
                i.Id,
                i.Nome,
                Receitas = i.ReceitaIngredientes.Select(ri => new
                {
                    ri.Receita.Id,
                    ri.Receita.Nome,
                    ri.Quantidade
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (ingrediente == null)
            return NotFound();

        return Ok(ingrediente);
    }

    // GET: api/ingredientes/populares
    [HttpGet("populares")]
    public async Task<ActionResult<IEnumerable<object>>> GetIngredientesPopulares([FromQuery] int limit = 10)
    {
        var ingredientes = await _context.ReceitaIngredientes
            .GroupBy(ri => new { ri.Ingrediente.Id, ri.Ingrediente.Nome })
            .Select(g => new
            {
                g.Key.Id,
                Nome = g.Key.Nome,
                QuantidadeReceitas = g.Count()
            })
            .OrderByDescending(x => x.QuantidadeReceitas)
            .Take(limit)
            .ToListAsync();

        return Ok(ingredientes);
    }

    // POST: api/ingredientes
    [HttpPost]
    public async Task<ActionResult<Ingrediente>> CreateIngrediente(IngredienteDto dto)
    {
        var ingrediente = new Ingrediente
        {
            Nome = dto.Nome
        };

        _context.Ingredientes.Add(ingrediente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetIngrediente), new { id = ingrediente.Id }, ingrediente);
    }

    // DELETE: api/ingredientes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngrediente(int id)
    {
        var ingrediente = await _context.Ingredientes.FindAsync(id);
        if (ingrediente == null)
            return NotFound();

        _context.Ingredientes.Remove(ingrediente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

// DTOs
public record IngredienteDto(string Nome);
