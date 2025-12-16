using BackendProject.App.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Data;

public static class DataSeeder
{
    public static void SeedData(AppDbContext db)
    {
        if (db.Usuarios.Any())
            return;

        // Criar usuários
        var usuario1 = new Usuario 
        { 
            Nome = "Maria Silva",
            Apelido = "Maria Chef",
            Email = "maria@exemplo.com", 
            Senha = "senha123",
            Instagram = "@maria_cozinha",
            Whatsapp = "+55 11 98765-4321"
        };
        
        var usuario2 = new Usuario 
        { 
            Nome = "João Santos",
            Apelido = "João Sabor",
            Email = "joao@exemplo.com", 
            Senha = "senha456",
            TikTok = "@joao_chef"
        };

        var usuario3 = new Usuario
        {
            Nome = "Ana Paula",
            Apelido = "Ana Cozinha",
            Email = "ana@exemplo.com",
            Senha = "senha789"
        };

        db.Usuarios.AddRange(usuario1, usuario2, usuario3);
        db.SaveChanges();

        // Criar ingredientes
        var ovo = new Ingrediente { Nome = "Ovo" };
        var farinha = new Ingrediente { Nome = "Farinha de trigo" };
        var acucar = new Ingrediente { Nome = "Açúcar" };
        var leite = new Ingrediente { Nome = "Leite" };
        var tomate = new Ingrediente { Nome = "Tomate" };
        var cebola = new Ingrediente { Nome = "Cebola" };
        var alho = new Ingrediente { Nome = "Alho" };

        db.Ingredientes.AddRange(ovo, farinha, acucar, leite, tomate, cebola, alho);
        db.SaveChanges();

        // Criar receitas
        var receitaBolo = new Receita
        {
            Nome = "Bolo de Chocolate",
            ModoPreparo = "1. Bata os ovos com açúcar\n2. Adicione farinha e leite\n3. Asse por 40min a 180°C",
            UsuarioId = usuario1.Id
        };

        var receitaMolho = new Receita
        {
            Nome = "Molho de Tomate Caseiro",
            ModoPreparo = "1. Refogue alho e cebola\n2. Adicione tomates picados\n3. Tempere e cozinhe por 20min",
            UsuarioId = usuario2.Id
        };

        db.Receitas.AddRange(receitaBolo, receitaMolho);
        db.SaveChanges();

        // Criar publicações automaticamente para cada receita
        var publicacaoBolo = new PublicacaoReceita
        {
            ReceitaId = receitaBolo.Id,
            DataPublicacao = DateTime.Now.AddDays(-10),
            NotaMedia = 4.5m,
            QuantidadeVotos = 25,
            Deliciosos = 18
        };

        var publicacaoMolho = new PublicacaoReceita
        {
            ReceitaId = receitaMolho.Id,
            DataPublicacao = DateTime.Now.AddDays(-5),
            NotaMedia = 4.8m,
            QuantidadeVotos = 32,
            Deliciosos = 28
        };

        db.PublicacoesReceitas.AddRange(publicacaoBolo, publicacaoMolho);
        db.SaveChanges();

        // Relacionar ingredientes com receitas (N:N)
        db.ReceitaIngredientes.AddRange(
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = ovo.Id, Quantidade = "3 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = farinha.Id, Quantidade = "2 xícaras" },
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = acucar.Id, Quantidade = "1 xícara" },
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = leite.Id, Quantidade = "200ml" },
            
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = tomate.Id, Quantidade = "5 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = cebola.Id, Quantidade = "1 unidade" },
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = alho.Id, Quantidade = "3 dentes" }
        );
        db.SaveChanges();

        // Criar interações de usuários: "Delicioso" e "Amei"
        db.ReceitasDeliciosas.Add(new ReceitaDeliciosa
        {
            UsuarioId = usuario3.Id,
            ReceitaId = receitaBolo.Id,
            DataMarcacao = DateTime.Now.AddDays(-3)
        });

        db.ReceitasAmadas.Add(new ReceitaAmada
        {
            UsuarioId = usuario2.Id,
            ReceitaId = receitaBolo.Id,
            DataMarcacao = DateTime.Now.AddDays(-2)
        });

        db.ReceitasAmadas.Add(new ReceitaAmada
        {
            UsuarioId = usuario3.Id,
            ReceitaId = receitaMolho.Id,
            DataMarcacao = DateTime.Now.AddDays(-1)
        });

        db.ReceitasDeliciosas.Add(new ReceitaDeliciosa
        {
            UsuarioId = usuario1.Id,
            ReceitaId = receitaMolho.Id,
            DataMarcacao = DateTime.Now
        });

        db.SaveChanges();
    }
}
