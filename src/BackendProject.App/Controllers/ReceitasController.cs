using BackendProject.App.Data;
using BackendProject.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceitasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReceitasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/receitas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetReceitas()
    {
        var receitas = await _context.Receitas
            .Include(r => r.Usuario)
            .Include(r => r.Publicacao)
            .Include(r => r.ReceitaIngredientes)
                .ThenInclude(ri => ri.Ingrediente)
            .OrderBy(r => r.Nome)
            .Select(r => new
            {
                r.Id,
                r.Nome,
                r.ModoPreparo,
                Usuario = new
                {
                    r.Usuario.Id,
                    r.Usuario.Nome,
                    r.Usuario.Apelido,
                    r.Usuario.Instagram,
                    r.Usuario.TikTok
                },
                Publicacao = r.Publicacao == null ? null : new
                {
                    r.Publicacao.DataPublicacao,
                    r.Publicacao.NotaMedia,
                    r.Publicacao.QuantidadeVotos,
                    r.Publicacao.Deliciosos
                },
                Ingredientes = r.ReceitaIngredientes.Select(ri => new
                {
                    ri.Ingrediente.Id,
                    ri.Ingrediente.Nome,
                    ri.Quantidade
                }).ToList()
            })
            .ToListAsync();

        return Ok(receitas);
    }

    // GET: api/receitas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetReceita(int id)
    {
        var receita = await _context.Receitas
            .Include(r => r.Usuario)
            .Include(r => r.Publicacao)
            .Include(r => r.ReceitaIngredientes)
                .ThenInclude(ri => ri.Ingrediente)
            .Where(r => r.Id == id)
            .Select(r => new
            {
                r.Id,
                r.Nome,
                r.ModoPreparo,
                Usuario = new
                {
                    r.Usuario.Id,
                    r.Usuario.Nome,
                    r.Usuario.Apelido,
                    r.Usuario.Email,
                    r.Usuario.Instagram,
                    r.Usuario.TikTok,
                    r.Usuario.Whatsapp
                },
                Publicacao = r.Publicacao == null ? null : new
                {
                    r.Publicacao.DataPublicacao,
                    r.Publicacao.NotaMedia,
                    r.Publicacao.QuantidadeVotos,
                    r.Publicacao.Deliciosos
                },
                Ingredientes = r.ReceitaIngredientes.Select(ri => new
                {
                    ri.Ingrediente.Id,
                    ri.Ingrediente.Nome,
                    ri.Quantidade
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (receita == null)
            return NotFound();

        return Ok(receita);
    }

    // GET: api/receitas/top
    [HttpGet("top")]
    public async Task<ActionResult<IEnumerable<object>>> GetTopReceitas([FromQuery] int limit = 10)
    {
        var receitas = await _context.PublicacoesReceitas
            .Include(p => p.Receita)
                .ThenInclude(r => r.Usuario)
            .OrderByDescending(p => p.NotaMedia)
            .Take(limit)
            .Select(p => new
            {
                p.Receita.Id,
                p.Receita.Nome,
                p.NotaMedia,
                p.QuantidadeVotos,
                p.Deliciosos,
                Usuario = new
                {
                    p.Receita.Usuario.Id,
                    p.Receita.Usuario.Apelido
                }
            })
            .ToListAsync();

        return Ok(receitas);
    }

    // POST: api/receitas
    [HttpPost]
    public async Task<ActionResult<Receita>> CreateReceita(ReceitaCreateDto dto)
    {
        var receita = new Receita
        {
            Nome = dto.Nome,
            ModoPreparo = dto.ModoPreparo,
            UsuarioId = dto.UsuarioId
        };

        _context.Receitas.Add(receita);
        await _context.SaveChangesAsync();

        // Criar publicação
        var publicacao = new PublicacaoReceita
        {
            ReceitaId = receita.Id,
            DataPublicacao = DateTime.Now,
            NotaMedia = 0,
            QuantidadeVotos = 0,
            Deliciosos = 0
        };

        _context.PublicacoesReceitas.Add(publicacao);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReceita), new { id = receita.Id }, receita);
    }

    // PUT: api/receitas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReceita(int id, ReceitaUpdateDto dto)
    {
        var receita = await _context.Receitas.FindAsync(id);
        if (receita == null)
            return NotFound();

        receita.Nome = dto.Nome;
        receita.ModoPreparo = dto.ModoPreparo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/receitas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReceita(int id)
    {
        var receita = await _context.Receitas.FindAsync(id);
        if (receita == null)
            return NotFound();

        _context.Receitas.Remove(receita);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

// DTOs
public record ReceitaCreateDto(string Nome, string ModoPreparo, int UsuarioId);
public record ReceitaUpdateDto(string Nome, string ModoPreparo);
