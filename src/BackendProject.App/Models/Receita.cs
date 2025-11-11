namespace BackendProject.App.Models;

public class Receita
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string ModoPreparo { get; set; } = string.Empty;
    
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    
    // Relacionamento 1:1 com PublicacaoReceita (criada automaticamente)
    public PublicacaoReceita? Publicacao { get; set; }
    
    public ICollection<ReceitaIngrediente> ReceitaIngredientes { get; set; } = new List<ReceitaIngrediente>();
    
    // Relacionamentos N:N para engajamento
    public ICollection<ReceitaDeliciosa> UsuariosQueAprovaram { get; set; } = new List<ReceitaDeliciosa>();
    public ICollection<ReceitaAmada> UsuariosQueAmaram { get; set; } = new List<ReceitaAmada>();
}
