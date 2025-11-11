namespace BackendProject.App.Models;

public class Usuario
{
    public int Id { get; set; }
    
    // Campos obrigatórios
    public string Nome { get; set; } = string.Empty;
    public string Apelido { get; set; } = string.Empty; // Aparece nas publicações
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    
    // Campos opcionais (redes sociais)
    public string? Whatsapp { get; set; }
    public string? Instagram { get; set; }
    public string? TikTok { get; set; }
    
    // Relacionamento: Um usuário pode ter várias receitas
    public ICollection<Receita> Receitas { get; set; } = new List<Receita>();
    
    // Relacionamentos N:N para engajamento
    public ICollection<ReceitaDeliciosa> ReceitasDeliciosas { get; set; } = new List<ReceitaDeliciosa>();
    public ICollection<ReceitaAmada> ReceitasAmadas { get; set; } = new List<ReceitaAmada>();
}
