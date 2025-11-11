namespace BackendProject.App.Models;

/// <summary>
/// Tabela de junção N:N entre Usuario e Receita.
/// Representa receitas que o usuário marcou como "Delicioso" (fez e aprovou).
/// </summary>
public class ReceitaDeliciosa
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    public int ReceitaId { get; set; }
    public Receita Receita { get; set; } = null!;
    
    // Data em que o usuário marcou como delicioso
    public DateTime DataMarcacao { get; set; }
}
