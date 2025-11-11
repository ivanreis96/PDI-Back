namespace BackendProject.App.Models;

/// <summary>
/// Representa a publicação de uma receita no estilo blog.
/// Criada automaticamente quando uma Receita é criada (1:1).
/// Armazena dados de engajamento: avaliações e contador "Delicioso".
/// </summary>
public class PublicacaoReceita
{
    public int Id { get; set; }
    
    // FK 1:1 com Receita (a publicação pertence à receita, e o autor vem da Receita.Usuario)
    public int ReceitaId { get; set; }
    public Receita Receita { get; set; } = null!;
    
    // Metadados da publicação
    public DateTime DataPublicacao { get; set; }
    
    // Avaliação: nota média de 1 a 5
    public decimal NotaMedia { get; set; } // Calculada a partir dos votos
    public int QuantidadeVotos { get; set; } // Total de pessoas que avaliaram
    
    // Contador "Delicioso" - usuários que fizeram e aprovaram a receita
    public int Deliciosos { get; set; }
}
