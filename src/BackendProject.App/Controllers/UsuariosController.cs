using BackendProject.App.Data;
using BackendProject.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetUsuarios()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.Receitas)
            .OrderBy(u => u.Nome)
            .Select(u => new
            {
                u.Id,
                u.Nome,
                u.Apelido,
                u.Email,
                u.Instagram,
                u.TikTok,
                u.Whatsapp,
                QuantidadeReceitas = u.Receitas.Count
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    // GET: api/usuarios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Receitas)
                .ThenInclude(r => r.Publicacao)
            .Where(u => u.Id == id)
            .Select(u => new
            {
                u.Id,
                u.Nome,
                u.Apelido,
                u.Email,
                u.Instagram,
                u.TikTok,
                u.Whatsapp,
                Receitas = u.Receitas.Select(r => new
                {
                    r.Id,
                    r.Nome,
                    NotaMedia = r.Publicacao != null ? r.Publicacao.NotaMedia : 0
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    // POST: api/usuarios
    [HttpPost]
    public async Task<ActionResult<Usuario>> CreateUsuario(UsuarioCreateDto dto)
    {
        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Apelido = dto.Apelido,
            Email = dto.Email,
            Senha = dto.Senha,
            Instagram = dto.Instagram,
            TikTok = dto.TikTok,
            Whatsapp = dto.Whatsapp
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
    }

    // PUT: api/usuarios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, UsuarioUpdateDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
            return NotFound();

        usuario.Nome = dto.Nome;
        usuario.Apelido = dto.Apelido;
        usuario.Email = dto.Email;
        usuario.Instagram = dto.Instagram;
        usuario.TikTok = dto.TikTok;
        usuario.Whatsapp = dto.Whatsapp;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/usuarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
            return NotFound();

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

// DTOs
public record UsuarioCreateDto(
    string Nome,
    string Apelido,
    string Email,
    string Senha,
    string? Instagram = null,
    string? TikTok = null,
    string? Whatsapp = null
);

public record UsuarioUpdateDto(
    string Nome,
    string Apelido,
    string Email,
    string? Instagram = null,
    string? TikTok = null,
    string? Whatsapp = null
);
