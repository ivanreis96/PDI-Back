namespace BackendProject.App.Models;

/// <summary>
/// Tabela de junção N:N entre Usuario e Receita.
/// Representa receitas que o usuário favoritou/curtiu (marcou como "Amei").
/// Funciona como lista de favoritos pessoal do usuário.
/// </summary>
public class ReceitaAmada
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public int ReceitaId { get; set; }
    public Receita Receita { get; set; } = null!;
    
    // Data em que o usuário marcou como amei/favorito
    public DateTime DataMarcacao { get; set; }
}
