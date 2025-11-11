namespace BackendProject.App.Models;

/// <summary>
/// Tabela de junção para relacionamento N:N entre Receita e Ingrediente.
/// Permite adicionar informações extras como quantidade.
/// </summary>
public class ReceitaIngrediente
{
    public int ReceitaId { get; set; }
    public Receita Receita { get; set; } = null!;
    
    public int IngredienteId { get; set; }
    public Ingrediente Ingrediente { get; set; } = null!;
    
    // Informação adicional: quantidade do ingrediente na receita
    public string Quantidade { get; set; } = string.Empty; // ex: "200g", "1 xícara"
}
