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
        var manteiga = new Ingrediente { Nome = "Manteiga" };
        var queijo = new Ingrediente { Nome = "Queijo" };
        var frango = new Ingrediente { Nome = "Peito de frango" };
        var arroz = new Ingrediente { Nome = "Arroz" };
        var feijao = new Ingrediente { Nome = "Feijão" };
        var batata = new Ingrediente { Nome = "Batata" };
        var cenoura = new Ingrediente { Nome = "Cenoura" };
        var brocolis = new Ingrediente { Nome = "Brócolis" };
        var azeite = new Ingrediente { Nome = "Azeite" };
        var sal = new Ingrediente { Nome = "Sal" };
        var pimenta = new Ingrediente { Nome = "Pimenta" };
        var chocolate = new Ingrediente { Nome = "Chocolate em pó" };
        var carne = new Ingrediente { Nome = "Carne moída" };

        db.Ingredientes.AddRange(ovo, farinha, acucar, leite, tomate, cebola, alho, manteiga, 
            queijo, frango, arroz, feijao, batata, cenoura, brocolis, azeite, sal, pimenta, chocolate, carne);
        db.SaveChanges();

        // Criar receitas
        var receitaBolo = new Receita
        {
            Nome = "Bolo de Chocolate",
            ModoPreparo = "1. Bata os ovos com açúcar\n2. Adicione farinha e leite\n3. Misture o chocolate em pó\n4. Asse por 40min a 180°C",
            UsuarioId = usuario1.Id
        };

        var receitaMolho = new Receita
        {
            Nome = "Molho de Tomate Caseiro",
            ModoPreparo = "1. Refogue alho e cebola no azeite\n2. Adicione tomates picados\n3. Tempere com sal e pimenta\n4. Cozinhe por 20min",
            UsuarioId = usuario2.Id
        };

        var receitaFrango = new Receita
        {
            Nome = "Frango Grelhado com Legumes",
            ModoPreparo = "1. Tempere o frango com sal e pimenta\n2. Grelhe em fogo médio por 15 minutos\n3. Refogue os legumes com alho e azeite\n4. Sirva junto",
            UsuarioId = usuario3.Id
        };

        var receitaRisoto = new Receita
        {
            Nome = "Risoto de Queijo",
            ModoPreparo = "1. Refogue cebola e alho\n2. Adicione o arroz e vá acrescentando água aos poucos\n3. Quando cremoso, adicione queijo e manteiga\n4. Misture bem e sirva",
            UsuarioId = usuario1.Id
        };

        var receitaStrogonoff = new Receita
        {
            Nome = "Strogonoff de Frango",
            ModoPreparo = "1. Corte o frango em cubos e tempere\n2. Refogue cebola e alho\n3. Adicione o frango e deixe dourar\n4. Acrescente creme de leite e ketchup\n5. Finalize com queijo ralado",
            UsuarioId = usuario2.Id
        };

        var receitaPanqueca = new Receita
        {
            Nome = "Panqueca de Frango",
            ModoPreparo = "1. Faça a massa com ovos, farinha e leite\n2. Prepare o recheio com frango desfiado e molho de tomate\n3. Monte as panquecas\n4. Cubra com queijo e leve ao forno",
            UsuarioId = usuario3.Id
        };

        var receitaSopa = new Receita
        {
            Nome = "Sopa de Legumes",
            ModoPreparo = "1. Corte todos os legumes em cubos\n2. Refogue alho e cebola\n3. Adicione os legumes e água\n4. Cozinhe até ficarem macios\n5. Tempere e sirva quente",
            UsuarioId = usuario1.Id
        };

        var receitaBolo2 = new Receita
        {
            Nome = "Bolo de Cenoura",
            ModoPreparo = "1. Bata no liquidificador cenoura, ovos e óleo\n2. Adicione açúcar e farinha\n3. Despeje em forma untada\n4. Asse por 40min\n5. Faça cobertura de chocolate",
            UsuarioId = usuario2.Id
        };

        var receitaLasanha = new Receita
        {
            Nome = "Lasanha à Bolonhesa",
            ModoPreparo = "1. Prepare o molho bolonhesa com carne moída\n2. Monte camadas de massa, molho e queijo\n3. Repita até preencher a forma\n4. Cubra com queijo\n5. Asse por 45min a 180°C",
            UsuarioId = usuario3.Id
        };

        var receitaPure = new Receita
        {
            Nome = "Purê de Batata Cremoso",
            ModoPreparo = "1. Cozinhe as batatas até ficarem macias\n2. Amasse bem\n3. Adicione leite, manteiga e sal\n4. Misture até ficar cremoso\n5. Sirva quente",
            UsuarioId = usuario1.Id
        };

        db.Receitas.AddRange(receitaBolo, receitaMolho, receitaFrango, receitaRisoto, 
            receitaStrogonoff, receitaPanqueca, receitaSopa, receitaBolo2, receitaLasanha, receitaPure);
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

        var publicacaoFrango = new PublicacaoReceita
        {
            ReceitaId = receitaFrango.Id,
            DataPublicacao = DateTime.Now.AddDays(-15),
            NotaMedia = 4.9m,
            QuantidadeVotos = 45,
            Deliciosos = 40
        };

        var publicacaoRisoto = new PublicacaoReceita
        {
            ReceitaId = receitaRisoto.Id,
            DataPublicacao = DateTime.Now.AddDays(-8),
            NotaMedia = 4.7m,
            QuantidadeVotos = 38,
            Deliciosos = 30
        };

        var publicacaoStrogonoff = new PublicacaoReceita
        {
            ReceitaId = receitaStrogonoff.Id,
            DataPublicacao = DateTime.Now.AddDays(-12),
            NotaMedia = 4.6m,
            QuantidadeVotos = 29,
            Deliciosos = 22
        };

        var publicacaoPanqueca = new PublicacaoReceita
        {
            ReceitaId = receitaPanqueca.Id,
            DataPublicacao = DateTime.Now.AddDays(-6),
            NotaMedia = 4.8m,
            QuantidadeVotos = 35,
            Deliciosos = 31
        };

        var publicacaoSopa = new PublicacaoReceita
        {
            ReceitaId = receitaSopa.Id,
            DataPublicacao = DateTime.Now.AddDays(-20),
            NotaMedia = 4.4m,
            QuantidadeVotos = 20,
            Deliciosos = 15
        };

        var publicacaoBolo2 = new PublicacaoReceita
        {
            ReceitaId = receitaBolo2.Id,
            DataPublicacao = DateTime.Now.AddDays(-3),
            NotaMedia = 4.9m,
            QuantidadeVotos = 50,
            Deliciosos = 45
        };

        var publicacaoLasanha = new PublicacaoReceita
        {
            ReceitaId = receitaLasanha.Id,
            DataPublicacao = DateTime.Now.AddDays(-7),
            NotaMedia = 4.7m,
            QuantidadeVotos = 42,
            Deliciosos = 35
        };

        var publicacaoPure = new PublicacaoReceita
        {
            ReceitaId = receitaPure.Id,
            DataPublicacao = DateTime.Now.AddDays(-9),
            NotaMedia = 4.3m,
            QuantidadeVotos = 18,
            Deliciosos = 12
        };

        db.PublicacoesReceitas.AddRange(publicacaoBolo, publicacaoMolho, publicacaoFrango,
            publicacaoRisoto, publicacaoStrogonoff, publicacaoPanqueca, publicacaoSopa,
            publicacaoBolo2, publicacaoLasanha, publicacaoPure);
        db.SaveChanges();

        // Relacionar ingredientes com receitas (N:N)
        db.ReceitaIngredientes.AddRange(
            // Bolo de Chocolate
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = ovo.Id, Quantidade = "3 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = farinha.Id, Quantidade = "2 xícaras" },
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = acucar.Id, Quantidade = "1 xícara" },
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = leite.Id, Quantidade = "200ml" },
            new ReceitaIngrediente { ReceitaId = receitaBolo.Id, IngredienteId = chocolate.Id, Quantidade = "4 colheres" },
            
            // Molho de Tomate
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = tomate.Id, Quantidade = "5 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = cebola.Id, Quantidade = "1 unidade" },
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = alho.Id, Quantidade = "3 dentes" },
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = azeite.Id, Quantidade = "2 colheres" },
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = sal.Id, Quantidade = "a gosto" },
            new ReceitaIngrediente { ReceitaId = receitaMolho.Id, IngredienteId = pimenta.Id, Quantidade = "a gosto" },

            // Frango Grelhado
            new ReceitaIngrediente { ReceitaId = receitaFrango.Id, IngredienteId = frango.Id, Quantidade = "500g" },
            new ReceitaIngrediente { ReceitaId = receitaFrango.Id, IngredienteId = cenoura.Id, Quantidade = "2 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaFrango.Id, IngredienteId = brocolis.Id, Quantidade = "1 maço" },
            new ReceitaIngrediente { ReceitaId = receitaFrango.Id, IngredienteId = alho.Id, Quantidade = "2 dentes" },
            new ReceitaIngrediente { ReceitaId = receitaFrango.Id, IngredienteId = azeite.Id, Quantidade = "2 colheres" },
            new ReceitaIngrediente { ReceitaId = receitaFrango.Id, IngredienteId = sal.Id, Quantidade = "a gosto" },
            new ReceitaIngrediente { ReceitaId = receitaFrango.Id, IngredienteId = pimenta.Id, Quantidade = "a gosto" },

            // Risoto
            new ReceitaIngrediente { ReceitaId = receitaRisoto.Id, IngredienteId = arroz.Id, Quantidade = "2 xícaras" },
            new ReceitaIngrediente { ReceitaId = receitaRisoto.Id, IngredienteId = queijo.Id, Quantidade = "200g" },
            new ReceitaIngrediente { ReceitaId = receitaRisoto.Id, IngredienteId = manteiga.Id, Quantidade = "50g" },
            new ReceitaIngrediente { ReceitaId = receitaRisoto.Id, IngredienteId = cebola.Id, Quantidade = "1 unidade" },
            new ReceitaIngrediente { ReceitaId = receitaRisoto.Id, IngredienteId = alho.Id, Quantidade = "2 dentes" },

            // Strogonoff
            new ReceitaIngrediente { ReceitaId = receitaStrogonoff.Id, IngredienteId = frango.Id, Quantidade = "600g" },
            new ReceitaIngrediente { ReceitaId = receitaStrogonoff.Id, IngredienteId = cebola.Id, Quantidade = "1 unidade" },
            new ReceitaIngrediente { ReceitaId = receitaStrogonoff.Id, IngredienteId = alho.Id, Quantidade = "2 dentes" },
            new ReceitaIngrediente { ReceitaId = receitaStrogonoff.Id, IngredienteId = leite.Id, Quantidade = "200ml" },
            new ReceitaIngrediente { ReceitaId = receitaStrogonoff.Id, IngredienteId = queijo.Id, Quantidade = "100g" },

            // Panqueca
            new ReceitaIngrediente { ReceitaId = receitaPanqueca.Id, IngredienteId = ovo.Id, Quantidade = "2 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaPanqueca.Id, IngredienteId = farinha.Id, Quantidade = "1 xícara" },
            new ReceitaIngrediente { ReceitaId = receitaPanqueca.Id, IngredienteId = leite.Id, Quantidade = "300ml" },
            new ReceitaIngrediente { ReceitaId = receitaPanqueca.Id, IngredienteId = frango.Id, Quantidade = "300g" },
            new ReceitaIngrediente { ReceitaId = receitaPanqueca.Id, IngredienteId = tomate.Id, Quantidade = "3 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaPanqueca.Id, IngredienteId = queijo.Id, Quantidade = "150g" },

            // Sopa
            new ReceitaIngrediente { ReceitaId = receitaSopa.Id, IngredienteId = batata.Id, Quantidade = "3 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaSopa.Id, IngredienteId = cenoura.Id, Quantidade = "2 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaSopa.Id, IngredienteId = cebola.Id, Quantidade = "1 unidade" },
            new ReceitaIngrediente { ReceitaId = receitaSopa.Id, IngredienteId = alho.Id, Quantidade = "2 dentes" },
            new ReceitaIngrediente { ReceitaId = receitaSopa.Id, IngredienteId = sal.Id, Quantidade = "a gosto" },

            // Bolo de Cenoura
            new ReceitaIngrediente { ReceitaId = receitaBolo2.Id, IngredienteId = cenoura.Id, Quantidade = "3 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaBolo2.Id, IngredienteId = ovo.Id, Quantidade = "4 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaBolo2.Id, IngredienteId = farinha.Id, Quantidade = "2 xícaras" },
            new ReceitaIngrediente { ReceitaId = receitaBolo2.Id, IngredienteId = acucar.Id, Quantidade = "1,5 xícara" },
            new ReceitaIngrediente { ReceitaId = receitaBolo2.Id, IngredienteId = chocolate.Id, Quantidade = "200g" },

            // Lasanha
            new ReceitaIngrediente { ReceitaId = receitaLasanha.Id, IngredienteId = carne.Id, Quantidade = "500g" },
            new ReceitaIngrediente { ReceitaId = receitaLasanha.Id, IngredienteId = tomate.Id, Quantidade = "4 unidades" },
            new ReceitaIngrediente { ReceitaId = receitaLasanha.Id, IngredienteId = queijo.Id, Quantidade = "300g" },
            new ReceitaIngrediente { ReceitaId = receitaLasanha.Id, IngredienteId = cebola.Id, Quantidade = "1 unidade" },
            new ReceitaIngrediente { ReceitaId = receitaLasanha.Id, IngredienteId = alho.Id, Quantidade = "3 dentes" },

            // Purê
            new ReceitaIngrediente { ReceitaId = receitaPure.Id, IngredienteId = batata.Id, Quantidade = "1kg" },
            new ReceitaIngrediente { ReceitaId = receitaPure.Id, IngredienteId = leite.Id, Quantidade = "200ml" },
            new ReceitaIngrediente { ReceitaId = receitaPure.Id, IngredienteId = manteiga.Id, Quantidade = "50g" },
            new ReceitaIngrediente { ReceitaId = receitaPure.Id, IngredienteId = sal.Id, Quantidade = "a gosto" }
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
