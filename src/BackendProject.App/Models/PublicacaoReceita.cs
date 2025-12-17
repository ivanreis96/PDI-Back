namespace BackendProject.App.Models;

public class PublicacaoReceita
{
    public int Id { get; set; }
    public int ReceitaId { get; set; }
    public Receita Receita { get; set; } = null!;
    public DateTime DataPublicacao { get; set; }
    public decimal NotaMedia { get; set; }
    public int QuantidadeVotos { get; set; }
    public int Deliciosos { get; set; }
}
