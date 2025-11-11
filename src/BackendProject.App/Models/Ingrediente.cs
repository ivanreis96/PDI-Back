namespace BackendProject.App.Models;

public class Ingrediente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    
    // Relacionamento N:N com Receitas
    public ICollection<ReceitaIngrediente> ReceitaIngredientes { get; set; } = new List<ReceitaIngrediente>();
}
